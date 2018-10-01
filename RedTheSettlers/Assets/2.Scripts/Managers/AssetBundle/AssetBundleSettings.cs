using UnityEngine;

namespace RedTheSettlers.GameSystem
{
    public class AssetBundleSettings : MonoBehaviour
    {
        public const int HASHCODELINE = 6; //6번째 ReadLine에 Manifest의 Hash코드 라인을 읽게 된다.
        public const string ASSETBUNDLEDIRECTORY = "Bundle";

        private void Start()
        {
            SetWebpaths();
        }

        private void SetWebpaths()
        {
            /* AssetBundle */
            AssetBundleManager.Instance.WebPaths.Add((int)AssetBundleNumbers.Player, "");
            AssetBundleManager.Instance.WebPaths.Add((int)AssetBundleNumbers.Skill, "");
            AssetBundleManager.Instance.WebPaths.Add((int)AssetBundleNumbers.Enemy, "");
            AssetBundleManager.Instance.WebPaths.Add((int)AssetBundleNumbers.MiddleBoss1, "");
            AssetBundleManager.Instance.WebPaths.Add((int)AssetBundleNumbers.MiddleBoss2, "");
            AssetBundleManager.Instance.WebPaths.Add((int)AssetBundleNumbers.Boss, "");
            AssetBundleManager.Instance.WebPaths.Add((int)AssetBundleNumbers.Tile, "");
            AssetBundleManager.Instance.WebPaths.Add((int)AssetBundleNumbers.UI, "");

            /* Manifest */
            AssetBundleManager.Instance.WebManifest.Add((int)AssetBundleNumbers.Player, "");
            AssetBundleManager.Instance.WebManifest.Add((int)AssetBundleNumbers.Skill, "");
            AssetBundleManager.Instance.WebManifest.Add((int)AssetBundleNumbers.Enemy, "");
            AssetBundleManager.Instance.WebManifest.Add((int)AssetBundleNumbers.MiddleBoss1, "");
            AssetBundleManager.Instance.WebManifest.Add((int)AssetBundleNumbers.MiddleBoss2, "");
            AssetBundleManager.Instance.WebManifest.Add((int)AssetBundleNumbers.Boss, "");
            AssetBundleManager.Instance.WebManifest.Add((int)AssetBundleNumbers.Tile, "");
            AssetBundleManager.Instance.WebManifest.Add((int)AssetBundleNumbers.UI, "");
        }
    }
}