using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedTheSettlers.UnitTest
{
    public enum AssetBundleNumbers
    {
        Player,
        Skill,
        Enemy, // unload(false)
        Boss1,
        Boss2,
        Boss3,
        Tile, // unload(false)
        UI,
        objects, // 테스트용으로 사용 // 추후에 제거할 것
        canvas, // 테스트용으로 사용 // 추후에 제거할 것
    }

    /// <summary>
    /// 작성자 : 박지용
    /// 에셋번들 매니저에서 사용할 데이터들을 관리하는 스크립트
    /// </summary>
    public class AssetBundleDataTest : MonoBehaviour
    {
        public const int HashCodeLine = 6; //6번째 ReadLine에 Manifest의 Hash코드 라인을 읽게 된다.
        public const string assetBundleDirectory = "Assets/0.AssetBundles/";

        private void Start()
        {
            SetWebpaths();
        }

        private void SetWebpaths()
        {
            /* AssetBundle */
            AssetBundleManagerTest.Instance.WebPaths.Add((int)AssetBundleNumbers.Player, "");
            AssetBundleManagerTest.Instance.WebPaths.Add((int)AssetBundleNumbers.Skill, "");
            AssetBundleManagerTest.Instance.WebPaths.Add((int)AssetBundleNumbers.Enemy, "");
            AssetBundleManagerTest.Instance.WebPaths.Add((int)AssetBundleNumbers.Boss1, "");
            AssetBundleManagerTest.Instance.WebPaths.Add((int)AssetBundleNumbers.Boss2, "");
            AssetBundleManagerTest.Instance.WebPaths.Add((int)AssetBundleNumbers.Boss3, "");
            AssetBundleManagerTest.Instance.WebPaths.Add((int)AssetBundleNumbers.Tile, "");
            AssetBundleManagerTest.Instance.WebPaths.Add((int)AssetBundleNumbers.UI, "");
            AssetBundleManagerTest.Instance.WebPaths.Add((int)AssetBundleNumbers.canvas, "https://drive.google.com/uc?authuser=0&id=1AHBIgStWfP28ODXGaY3pxU2OslSTsl3Q&export=download");
            AssetBundleManagerTest.Instance.WebPaths.Add((int)AssetBundleNumbers.objects, "https://drive.google.com/uc?authuser=0&id=189GqP1ULCgaZDLq-x9VCrH3eCDOv9qpJ&export=download");
            
            /* Manifest */
            AssetBundleManagerTest.Instance.WebManifest.Add((int)AssetBundleNumbers.objects, "https://drive.google.com/uc?authuser=0&id=1qWeskKwcwEyh330RmR36GV4TReFJlIui&export=download");
            AssetBundleManagerTest.Instance.WebManifest.Add((int)AssetBundleNumbers.canvas, "https://drive.google.com/uc?authuser=0&id=174Bgh4SsX7koF9PVDNFWjYlUpS4ihPjE&export=download");
        }
    }
}