using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainScene.Store
{
    public class TipBox:MonoBehaviour
    {
        public Vector2 openedPosition;
        public Vector2 closedPosition;
        
        public Text tipText;
        
        private Coroutine _moveCoroutine;

        public void MakeMove(string tip)
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }

            transform.position = closedPosition;
            tipText.text = tip;
            StartCoroutine(MoveCoroutine());
        }
        
        private IEnumerator MoveCoroutine()
        {
            while (((Vector2)transform.position-openedPosition).magnitude>0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position,openedPosition,10);
                yield return null;
            }
            yield return new WaitForSeconds(2f);
            while (((Vector2)transform.position-closedPosition).magnitude>0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position,closedPosition,10);
                yield return null;
            }
        }
    }
}