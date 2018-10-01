using System;
using System.IO;
using UnityEngine;

namespace RedTheSettlers.GameSystem
{
    [Serializable]
    public struct AssetBundleData
    {
        public string Player;
        public string Skill;
        public string Enemey;
        public string FirstBoss;
        public string SecondBoss;
        public string ThirdBoss;
        public string UI;
        public string Tile;
    }
    /// <summary>
    /// 작성자 : 박준명
    /// </summary>
    public class DataManager : Singleton<DataManager>
    {
        public const string DirectoryName = "Data";
        public const string FilePath = DirectoryName + "/AssetBundleData.txt";
        public delegate void LoginResult(string Text);
        public LoginResult LoginResultCallback;
        public delegate void SignUpResult(string Text);
        public LoginResult SignUpResultCallback;

        private GameData gameData;

        public GameData GameData
        {
            get { return gameData; }
        }

        private GameDataLoader gameDataLoader;
        // Use this for initialization
        void Awake()
        {
            gameData = new GameData(4);
            gameDataLoader = new GameDataLoader();
            DontDestroyOnLoad(gameObject);
        }

        public void CreateNewAccount(string id, string password)
        {
            gameDataLoader.MakeNewAccountInDB(id, password);
        }

        public void Login(string id, string password)
        {
            gameDataLoader.LoadLoginDataFromDB(id, password);
        }

        public void SaveGameData(GameData gameData, bool ShouldSaveForDB)
        {
            this.gameData = gameData;

            if (ShouldSaveForDB)
            {
                gameDataLoader.SetUpdateInDB(gameData);
            }
        }

        public void ResetData()
        {
            gameData.PlayerData = new PlayerData[4];
            gameDataLoader.SetUpdateInDB(gameData);
        }

        /// <summary>
        /// AssetBundle의 키 값을 비교하여 업데이트 유무를 Bool형식으로 리턴함.(사용자는 꼭 Enum순서대로 함수를 사용할것. 
        /// ex) Player -> Skill(o) , Player -> Enemy(x))
        /// </summary>
        /// <param name="bundleNumbers"></param>
        /// <param name="assetBundleData"></param>
        /// <returns></returns>
        public bool CheckedBundleVersion(AssetBundleNumbers bundleNumbers, string assetBundleData)
        {
            if (!Directory.Exists(DirectoryName))
            {
                Directory.CreateDirectory(DirectoryName);
                LogManager.Instance.UserDebug(LogColor.Magenta, GetType().Name, "AssetBundle을 위한 Data폴더 생성");
            }

            FileInfo fileInfo = new FileInfo(FilePath);
            if(!fileInfo.Exists)
            {
                File.Create(FilePath).Dispose();
            }
            string[] lines;
            lines = File.ReadAllLines(FilePath);
            if (lines.Length - 1 < (int)bundleNumbers)
            {
                using (StreamWriter writer = new StreamWriter(FilePath, true))
                {
                    writer.WriteLine(assetBundleData);
                    LogManager.Instance.UserDebug(LogColor.Magenta, GetType().Name, "기록된 정보가 없음. 새로 입력.");   
                }
            return false;
            }
            else if (!lines[(int)bundleNumbers].Equals(assetBundleData))
            {
                using (StreamWriter writer = new StreamWriter(FilePath, false))
                {
                    lines[(int)bundleNumbers] = assetBundleData;
                    for (int i = 0; i < lines.Length; i++)
                        writer.WriteLine(lines[i]);
                    LogManager.Instance.UserDebug(LogColor.Magenta, GetType().Name, "기록된 정보와 불일치. 업데이트 필요.");
                }
            return false;
            }
            LogManager.Instance.UserDebug(LogColor.Magenta, GetType().Name, "기록된 정보와 일치. 업데이트 필요없음");
            return true;
        }
    }

}


