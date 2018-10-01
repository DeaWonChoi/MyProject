using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 작성자 : 김하정
/// </summary>
namespace RedTheSettlers
{
    namespace UI
    {
        public class UITitle : MonoBehaviour
        {
            public Text TitleText;
            private Color color;
            private float alpha;
            [SerializeField, Range(0, 2)]
            private float speed;

            private void Start()
            {
                StartCoroutine(FadeInCoroutine());
            }

            IEnumerator FadeInCoroutine()
            {
                float count = 0;
                while (true)
                {
                    alpha = Mathf.Sin(count * Mathf.Deg2Rad);
                    TitleText.color = new Color(TitleText.color.r, TitleText.color.g, TitleText.color.b, alpha);
                    count += Time.deltaTime * 90f * speed;

                    if (count > 180f)
                    {
                        count = 0;
                    }

                    yield return null;
                }
            }



        }
    }
}
