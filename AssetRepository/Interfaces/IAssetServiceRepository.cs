using AssetService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AssetRepository.Interfaces
{
   public interface IAssetServiceRepository
    {
        Task<IEnumerable<Asset>> SearchAssets(string properyName, bool value);
       
        Task<Asset> GetAsset(Int32 AssetId);

        Task<int> PostAsset(Asset AssetId);

        Task<int> UpdateAsset(int assetId, Asset Asset);

        Task<int> Processfile(string[] lines);

    }
}
