using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using RedTheSettlers.GameSystem;

namespace RedTheSettlers.UnitTest
{
    /// <summary>
    /// 작성자 : 박지용
    /// 게임에서 사용할 에셋번들을 관리하는 매니저
    /// </summary>
    public class AssetBundleManagerTest : Singleton<AssetBundleManagerTest>
    {
        private string assetBundleDirectory = AssetBundleDataTest.assetBundleDirectory;
        private int HashCodeLine = AssetBundleDataTest.HashCodeLine;

        public Dictionary<int, string> WebPaths = new Dictionary<int, string>();
        public Dictionary<int, string> WebManifest = new Dictionary<int, string>();
        private Dictionary<int, AssetBundle> Bundles = new Dictionary<int, AssetBundle>();

        //public const int HashCodeLine = 6; //6번째 ReadLine에 Manifest의 Hash코드 라인을 읽게 된다.
        //public const string assetBundleDirectory = "Assets/0.AssetBundles/";

        private void Awake()
        {
            DontDestroyOnLoad(this);
            //SetWebpaths();
        }

        //private void SetWebpaths()
        //{
        //    /* AssetBundle */
        //    WebPaths.Add((int)AssetBundleNumbers.Player, "");
        //    WebPaths.Add((int)AssetBundleNumbers.Skill, "");
        //    WebPaths.Add((int)AssetBundleNumbers.Enemy, "");
        //    WebPaths.Add((int)AssetBundleNumbers.Boss1, "");
        //    WebPaths.Add((int)AssetBundleNumbers.Boss2, "");
        //    WebPaths.Add((int)AssetBundleNumbers.Boss3, "");
        //    WebPaths.Add((int)AssetBundleNumbers.Tile, "");
        //    WebPaths.Add((int)AssetBundleNumbers.UI, "");
        //    WebPaths.Add((int)AssetBundleNumbers.canvas, "https://drive.google.com/uc?authuser=0&id=1AHBIgStWfP28ODXGaY3pxU2OslSTsl3Q&export=download");
        //    WebPaths.Add((int)AssetBundleNumbers.objects, "https://drive.google.com/uc?authuser=0&id=189GqP1ULCgaZDLq-x9VCrH3eCDOv9qpJ&export=download");

        //    /* Manifest */
        //    WebManifest.Add((int)AssetBundleNumbers.objects, "https://drive.google.com/uc?authuser=0&id=1qWeskKwcwEyh330RmR36GV4TReFJlIui&export=download");
        //    WebManifest.Add((int)AssetBundleNumbers.canvas, "https://drive.google.com/uc?authuser=0&id=174Bgh4SsX7koF9PVDNFWjYlUpS4ihPjE&export=download");
        //}

        private void AddBundles(AssetBundleNumbers num, AssetBundle bundle)
        {
            if (!Bundles.ContainsKey((int)num))
            {
                Bundles.Add((int)num, bundle);
            }
        }

        /// <summary>
        /// 정해진 웹주소에서 에셋번들을 다운 받는다. 
        /// 실사용시에는 매개변수에 AssetBundleNumbers를 받아 구분한다.
        /// </summary>
        public void DownLoadAssetBundle()
        {

        }

        public void DownCanvas()
        {
            StartCoroutine(SaveAssetBundleOnDisk(AssetBundleNumbers.canvas));
        }

        public void DownObjects()
        {
            StartCoroutine(SaveAssetBundleOnDisk(AssetBundleNumbers.objects));
        }
        /// <summary>
        /// 로컬 드라이브에서 에셋번들을 불러온다.
        /// 실사용시에는 매개변수에 AssetBundleNumbers를 받아 구분한다.
        /// </summary>
        public void LocalLoadAssetBundle()
        {

        }

        public void LoadCanvas()
        {
            StartCoroutine(LoadAssetBundleFromLocalDisk(AssetBundleNumbers.canvas));
        }

        public void LoadObjects()
        {
            StartCoroutine(LoadAssetBundleFromLocalDisk(AssetBundleNumbers.objects));
        }

        private string GetAssetBundlePath(AssetBundleNumbers key)
        {
            string bundleName = string.Empty;
            if (!WebPaths.TryGetValue((int)key, out bundleName))
            {
                LogManager.Instance.UserDebug(LogColor.Orange, "AssetBundleManager", "Dictionary에 존재하지 않는 번들입니다.");
                return null;
            }
            return bundleName;
        }

        private string GetManifestPath(AssetBundleNumbers key)
        {
            string bundleName = string.Empty;
            if (!WebManifest.TryGetValue((int)key, out bundleName))
            {
                LogManager.Instance.UserDebug(LogColor.Orange, "AssetBundleManager", "Dictionary에 존재하지 않는 메니페스트 파일입니다.");
                return null;
            }
            return bundleName;
        }

        private string GetAssetBundleName(AssetBundleNumbers number)
        {
            return number.ToString();
        }

        private IEnumerator SaveAssetBundleOnDisk(AssetBundleNumbers number)
        {
            string assetBundleName = GetAssetBundleName(number);
            string uri = GetAssetBundlePath(number);

            UnityWebRequest request = UnityWebRequest.Get(uri);
            yield return request.Send();

            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
                LogManager.Instance.UserDebug(LogColor.Orange, "AssetBundleManager", "AssetBundle Directory 생성");
            }
            else
            {
                LogManager.Instance.UserDebug(LogColor.Orange, "AssetBundleManager", "해당 폴더가 이미 존재합니다.");
            }

            FileStream fs = new FileStream(assetBundleDirectory + assetBundleName, FileMode.Create); // Create는 있으면 덮어씀, CreateNew는 새로 생성;
            fs.Write(request.downloadHandler.data, 0, (int)request.downloadedBytes);
            fs.Close();

            LogManager.Instance.UserDebug(LogColor.Orange, "AssetBundleManager", "다운로드 완료" + " " + request.downloadedBytes + "Bytes");
        }

        private IEnumerator LoadAssetBundleFromLocalDisk(AssetBundleNumbers number)
        {
            string assetBundleName = GetAssetBundleName(number);
            string uri = "file:///" + Application.dataPath + "/0.AssetBundles/" + assetBundleName;

            UnityWebRequest request = UnityWebRequest.GetAssetBundle(uri);
            yield return request.Send();

            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
            AddBundles(number, bundle);
        }

        public void CheckObjects()
        {
            StartCoroutine(CheckAssetBundleVersion(AssetBundleNumbers.objects));
        }

        public void CheckCanvas()
        {
            StartCoroutine(CheckAssetBundleVersion(AssetBundleNumbers.canvas));
        }

        /// <summary>
        /// 현재 에셋번들이 최신버전인지 체크한다. 
        /// </summary>
        public IEnumerator CheckAssetBundleVersion(AssetBundleNumbers number)
        {
            string assetBundleName = GetAssetBundleName(number) + ".manifest";
            string uri = GetManifestPath(number);

            Debug.Log(uri);

            UnityWebRequest request = UnityWebRequest.Get(uri);
            yield return request.Send();

            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
                LogManager.Instance.UserDebug(LogColor.Orange, "AssetBundleManager", "AssetBundle Directory 생성");
            }
            else
            {
                LogManager.Instance.UserDebug(LogColor.Orange, "AssetBundleManager", "해당 폴더가 이미 존재합니다.");
            }

            FileStream fs = new FileStream(assetBundleDirectory + assetBundleName, FileMode.Create); // Create는 있으면 덮어씀, CreateNew는 새로 생성;
            fs.Write(request.downloadHandler.data, 0, (int)request.downloadedBytes);
            fs.Close();

            LogManager.Instance.UserDebug(LogColor.Orange, "AssetBundleManager", "다운로드 완료" + " " + request.downloadedBytes + "Bytes");

            /////////////////////////////////////////////////////////////

            string newManifest = string.Empty;
            //string nowManifest = string.Empty; // DataManager에서 받아옴
            string tempHash = "e8e649b24e98b76009451b3b64b5e42e";

            
            fs = new FileStream(assetBundleDirectory + assetBundleName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);


            for (int i = 0; i < HashCodeLine; i++)
            {
                newManifest = sr.ReadLine();
                Debug.Log(i + ": " + newManifest);
            }
            sr.Close(); fs.Close();

            string[] split = newManifest.Split(' ');
            newManifest = split[split.Length - 1];
            Debug.Log(newManifest);

            if (newManifest.CompareTo(tempHash) == 0) Debug.Log("같은 파일");
            else Debug.Log("다른 파일. 에셋번들 다운로드");
        }

        public void CloneObjects()
        {
            AssetBundle bundle;
            Bundles.TryGetValue((int)AssetBundleNumbers.objects, out bundle);

            GameObject prefab;
            prefab = bundle.LoadAsset<GameObject>("cube"); Instantiate(prefab);
            prefab = bundle.LoadAsset<GameObject>("sphere"); Instantiate(prefab);

            Resources.UnloadUnusedAssets(); // 호출되지 않은 번들 제거
                                            //bundle.Unload(true);
        }

        public void CloneCanvas()
        {
            AssetBundle bundle;
            Bundles.TryGetValue((int)AssetBundleNumbers.canvas, out bundle);

            GameObject canvas;
            canvas = bundle.LoadAsset<GameObject>("canvas");
            Instantiate(canvas);
        }
    }
}