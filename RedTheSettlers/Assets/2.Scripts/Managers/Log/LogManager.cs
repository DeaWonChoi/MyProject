using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 담당자 : 박상원
/// Log 출력 및  기록을 전담
/// </summary>
namespace RedTheSettlers.GameSystem
{
    /// <summary>
    /// 디버그 LogColor 선택을 위한 Type 값
    /// 자신의 색상값 잘 기억해둘 것
    /// </summary>
    public enum LogColor
    {
        Orange, // 지용
        Olive,
        Green,
        Teal,
        Blue,
        Navy,
        Purple,
        Magenta, //준명
        Brown,
        Red, // 중요
    }

    public class LogManager : Singleton<LogManager>
    {

        private FileStream logFile;
        private FileInfo debugLog;
        private DirectoryInfo folderCheck;

        private string fileName;
        private string className;
        private string logText;

        private void Awake()
        {
            fileName = "Log-" + DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss") + ".txt";
            DontDestroyOnLoad(gameObject);
        }

        private void CreateLogCheck(string scriptName, object text)
        {
            if (folderCheck == null)
            {
                CreateLog(scriptName, text);
            }
            else
            {
                WriteLog(scriptName, text);
            }
        }

        private void CreateLog(string scriptName, object text)
        {
            folderCheck = new DirectoryInfo("./Log");
            folderCheck.Create();
            debugLog = new FileInfo("./log/" + fileName);

            if (!debugLog.Exists)
            {
                logFile = debugLog.Create();
                logFile.Close();
            }

            logText = DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss") + " [" + scriptName + "] " + "Log : " + text;
            File.AppendAllText("./log/" + fileName, logText + "\n");
        }

        private void WriteLog(string scriptName, object text)
        {
            logText = DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss") + " [" + scriptName + "] " + "Log : " + text;
            File.AppendAllText("./log/" + fileName, logText + "\n");
        }

        /// <summary>
        /// 프로젝트 진행 중 DebugLog 대체용 Log 메서드
        /// </summary>
        /// <param name="logColor">유니티 Console에 표시할 Log 색상</param>
        /// <param name="scriptName">Log 기록을 요청한 Script(클래스)의 명칭</param>
        /// <param name="text">기록할 로그 내용 " " 안에 원하는 내용 기입하는 것으로 내용 입력</param>
        public void UserDebug(LogColor logColor, string scriptName, object text)
        {
            Debug.Log("<color=" + logColor + ">" + text + "</color>");
            CreateLogCheck(scriptName, text);
        }
    }
}