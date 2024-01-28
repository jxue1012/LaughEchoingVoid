
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class ScreenTransitionUI : MonoBehaviour
{

    public Image image;
    private Material mat;
    private Sequence StartSequence;
    private Sequence EndSequence;

    Action eStartFunc;
    Action eEndFunc;


    public void Init()
    {
        this.gameObject.SetActive(false);
        ClearEvents();
        mat = image.material;
        Sequence s0 = DOTween.Sequence();

        s0.PrependCallback(() =>
        {
            this.gameObject.SetActive(true);
            var playerPos = GameCenter.Instance.playerManager.Player.HeadPoint.transform.position;
            var pos = Camera.main.WorldToScreenPoint(playerPos);
            float x = Mathf.Clamp(pos.x / Screen.width, 0, 1f);
            float y = Mathf.Clamp(pos.y / Screen.height, 0, 1f);
            mat.SetVector("_Center", new Vector4(x, y, 0f, 0f));
            mat.SetFloat("_Radius", 1.5f);
        })
        .Append(DOVirtual.Float(1.5f, 0f, 1f, value =>
        {
            mat.SetFloat("_Radius", value);
        }))
        .AppendCallback(() =>
        {
            eStartFunc?.Invoke();
            eStartFunc = null;
        })
        .SetAutoKill(false)
        .SetUpdate(true);

        StartSequence = s0;


        Sequence s1 = DOTween.Sequence();

        s1.PrependCallback(() =>
        {
            var playerPos = GameCenter.Instance.playerManager.Player.HeadPoint.transform.position;
            var pos = Camera.main.WorldToScreenPoint(playerPos);
            float x = Mathf.Clamp(pos.x / Screen.width, 0, 1f);
            float y = Mathf.Clamp(pos.y / Screen.height, 0, 1f);
            mat.SetVector("_Center", new Vector4(x, y, 0f, 0f));

        })
        .Append(DOVirtual.Float(0f, 1.5f, 1f, value =>
        {
            mat.SetFloat("_Radius", value);
        }))
        .AppendCallback(() =>
        {
            eEndFunc?.Invoke();
            this.gameObject.SetActive(false);
            eEndFunc = null;
        })
        .SetAutoKill(false)
        .SetUpdate(true);

        EndSequence = s1;
    }

    public void StartTransition(Action startFunc = null)
    {
        eStartFunc += startFunc;
        StartSequence.Restart();
    }

    public void EndTransition(Action endFunc = null)
    {
        eEndFunc += endFunc;
        EndSequence.Restart();
    }

    private void ClearEvents()
    {

        eEndFunc = null;
    }

}
