using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetRepository.Interfaces;
using AssetService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetServiceRepository assetServiceRepository;

        public AssetController(IAssetServiceRepository assetServiceRepository)
        {
            this.assetServiceRepository = assetServiceRepository;
        }
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Asset>>> Search(string Property, bool value)
        {
            try
            {
                var result = await assetServiceRepository.SearchAssets(Property, value);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }

        }

        // GET: api/Products/5
        [HttpGet("{assetId:int}")]
        public async Task<ActionResult<Asset>> GetAssets(int assetId)
        {
            try
            {
                var result = await assetServiceRepository.GetAsset(assetId);

                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }


        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Asset asset)
        {
            try
            {
                var assetid = await assetServiceRepository.PostAsset(asset);
                if (assetid > 0)
                {
                    return Ok(assetid);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding data to the database");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(int assetId, [FromBody] Asset asset)
        {
            try
            {
                var assetid = await assetServiceRepository.UpdateAsset(assetId, asset);
                if (assetid > 0)
                {
                    return Ok(assetid);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating data to the database");
            }
        }

        [HttpPut("{processfile}")]
        public async Task<IActionResult> Processfile()
        { 
            try
            {
                string path = "test.csv";
                string[] lines = System.IO.File.ReadAllLines(path);
                var rowcount = await assetServiceRepository.Processfile(lines);
                if (rowcount > 0)
                {
                    return Ok(rowcount);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating data to the database");
            }

        }


    }
}
