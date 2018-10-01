using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 작성자 : 김하정
/// 게임 로딩시 보여주는 씬을 위해 제작
/// </summary>
namespace RedTheSettlers.UI
{
    public class UILoading : MonoBehaviour
    {

        [SerializeField]
        private GameObject loadingObject;

        [SerializeField]
        private Text loadingText;
        private int speed = 150;
        private string[] loadingArray = new string[] { ".", "..", "..." };
        private int count = 0;

        void Update()
        {
            loadingObject.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * speed);
            DotMethod();
        }
        void DotMethod()
        {
            if (count < 120)
            {
                loadingText.text = "Loading" + loadingArray[(int)Mathf.Floor(count / 40)];
                count++;
            }
            else
                count = 0;

        }

    }

}
