using AssetRepository.Database;
using AssetRepository.Interfaces;
using AssetService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetRepository.Implementations
{
   public class AssetServiceRepository : IAssetServiceRepository
    {
        private readonly DatabaseContext databaseContext;
        public AssetServiceRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<IEnumerable<Asset>> SearchAssets(string PropertyName, bool value)
        {
            IQueryable<Asset> query = databaseContext.Assets;

            //if (AssetId > 0)
            //    query = query.Where(e => e.AssetId == AssetId);

            if (!string.IsNullOrEmpty(PropertyName))
            {
                switch (PropertyName)
                {
                    case "Is fix income":
                        query = query.Where(e => e.IsfixIncome == value);
                        break;
                    case "Is convertible":
                        query = query.Where(e => e.IsConvertible == value);
                        break;
                    case "Is swap":
                        query = query.Where(e => e.IsSwap == value);
                        break;
                    case "Is cash":
                        query = query.Where(e => e.IsCash == value);
                        break;
                    case "Is future":
                        query = query.Where(e => e.IsFuture == value);
                        break;
                    default:
                        break;
                }
            }
            return await query.ToListAsync();
        }
        public async Task<Asset> GetAsset(Int32 AssetId)
        {
            return await databaseContext.Assets.FirstOrDefaultAsync(e => e.AssetId == AssetId);
        }
        public async Task<int> PostAsset(Asset asset)
        {
            try
            {
                if (databaseContext != null)
                {
                    await databaseContext.Assets.AddAsync(asset);
                    await databaseContext.SaveChangesAsync();
                    return asset.AssetId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
           
            return 0;
        }
        public async Task<int> UpdateAsset(int assetId,Asset asset)
        {
            try
            {
                Asset query = databaseContext.Assets.FirstOrDefault(e => e.AssetId == assetId && e.Timestamp < asset.Timestamp);
                asset.AssetId = assetId;
                if (query != null)
                {
                    //var assetChanges = databaseContext.Assets.Attach(asset);
                    //assetChanges.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    //await databaseContext.SaveChangesAsync();

                    Asset assetobj = await databaseContext.Assets.Where(x => x.AssetId == asset.AssetId).SingleOrDefaultAsync();
                    assetobj.Name = asset.Name;
                    assetobj.IsCash = asset.IsCash;
                    assetobj.IsConvertible = asset.IsConvertible;
                    assetobj.IsfixIncome = asset.IsfixIncome;
                    assetobj.IsFuture = asset.IsFuture;
                    assetobj.IsSwap = asset.IsSwap;
                    assetobj.Timestamp = asset.Timestamp;

                    databaseContext.Assets.Update(assetobj);
                    await databaseContext.SaveChangesAsync();
                    return assetobj.AssetId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
                   
            return asset.AssetId;
        }
        public async Task<int> Processfile(string[] lines)
        {
            List<Asset> updateAssets = new List<Asset>();
            Asset assetobj = null;

            try
            {

                foreach (string line in lines)
                {
                    string updatedline = line.Replace("\"", "");

                    string[] columns = updatedline.Split(',');

                    //only record for which timestamp is greater than from last is upadate will be retrived.
                    Asset query = databaseContext.Assets.FirstOrDefault(e => e.AssetId == System.Convert.ToInt32(columns[0]) && e.Timestamp < System.Convert.ToDateTime(columns[3]));

                    if (query != null)
                    {
                        //var assetChanges = databaseContext.Assets.Attach(asset);
                        //assetChanges.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //await databaseContext.SaveChangesAsync();

                        assetobj = await databaseContext.Assets.Where(x => x.AssetId == System.Convert.ToInt32(columns[0])).SingleOrDefaultAsync();

                        var listQuery = updateAssets.Where(x => x.AssetId == System.Convert.ToInt32(columns[0]) && x.Timestamp < System.Convert.ToDateTime(columns[3]));

                        if (listQuery != null)
                        {
                            foreach (var asst in listQuery)
                            {
                                asst.AssetId = 35;
                            }
                        }
                        else
                        {

                            if (columns[1] == "is cash")
                            {
                                assetobj.IsCash = System.Convert.ToBoolean(columns[2]);
                            }


                            if (columns[1] == "is convertible")
                            {
                                assetobj.IsConvertible = System.Convert.ToBoolean(columns[2]);
                            }


                            if (columns[1] == "is fix income")
                            {
                                assetobj.IsfixIncome = System.Convert.ToBoolean(columns[2]);
                            }


                            if (columns[1] == "is future")
                            {
                                assetobj.IsFuture = System.Convert.ToBoolean(columns[2]);
                            }

                            if (columns[1] == "is swap")
                            {
                                assetobj.IsSwap = System.Convert.ToBoolean(columns[2]);
                            }
                            assetobj.Timestamp = System.Convert.ToDateTime(columns[3]);

                            updateAssets.Add(assetobj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }


            if (updateAssets.Count > 0)
            {
                databaseContext.Assets.Update(assetobj);
                await databaseContext.SaveChangesAsync();
            }
            return updateAssets.Count;


        }

      
    }
}
