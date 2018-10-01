using RedTheSettlers.GameSystem;
using UnityEngine;
using UnityEngine.UI;


namespace RedTheSettlers.UI
{
    /// <summary>
    /// 작성자 : 김하정, 박준명
    /// 로그인 UI를 위해 제작된 스크립트
    /// </summary>
    public class UILogin : MonoBehaviour
    {
        //[HideInInspector]
        [Header("Player Info")]
        [SerializeField]
        private InputField playerID;
        //[HideInInspector]
        [SerializeField]
        private InputField playerPassword;
        [SerializeField]
        private Text loginAlertText;
        [SerializeField]
        private InputField SignUpID;
        [SerializeField]
        private InputField SignUpPassword;
        [SerializeField]
        private InputField PasswordConfirmation;
        [SerializeField]
        private Text createAlertText;
        [SerializeField]
        private GameObject createResultObject;
        [SerializeField]
        private GameObject loginResultObject;
        [SerializeField]
        private GameObject ContinueGameObject;

        // Use this for initialization
        void Start()
        {
            loginAlertText.text = "";
            createAlertText.text = "";
            DataManager.Instance.LoginResultCallback = ChangeLoginAlertText;
            DataManager.Instance.SignUpResultCallback = ChangeCreateAlertText;
        }

        public void OnClickedGameQuitButton()
        {
            Application.Quit();
        }

        public void OnClickedLoginButton()
        {
            DataManager.Instance.Login(playerID.text, playerPassword.text);

        }

        public void OnClickedLoginSuccessButton()
        {
            if (DataManager.Instance.GameData.InGameData.TurnCount != 0)
            {
                ContinueGameObject.SetActive(true);
            }
            else
            {
                StageManager.Instance.ChangeState(StageType.LoadingScene);
            }
            
        }

        public void OnClickedContinueGameButton(bool wouldContinue)
        {
            if(wouldContinue)
                StageManager.Instance.JudgeLoadingData(true, StageType.BoardScene);
            else
                StageManager.Instance.JudgeLoadingData(false, StageType.BoardScene);
        }

        public void ChangeLoginAlertText(string Text)
        {
            if (Text.Equals("성공"))
            {
                loginResultObject.SetActive(true);
                loginAlertText.text = "";
            }
            else
                loginAlertText.text = Text;
        }

        public void ChangeCreateAlertText(string Text)
        {
            if (Text.Equals("성공"))
            {
                createResultObject.SetActive(true);
                createAlertText.text = "";
            }
            else
                createAlertText.text = Text;
        }

        public void OnClickedCreateAccountButton()
        {
            if (SignUpID.text.Length < 4)
            {
                createAlertText.text = "아이디는 최소 4자여야 합니다.";
            }
            else if (SignUpPassword.text.Length < 4)
            {
                createAlertText.text = "비밀번호는 최소 4자여야 합니다.";
            }
            else if (SignUpPassword.text != PasswordConfirmation.text)
                createAlertText.text = "비밀번호확인이 일치하지 않습니다.";
            else
            {
                DataManager.Instance.CreateNewAccount(SignUpID.text, SignUpPassword.text);
            }
        }
    }

}
