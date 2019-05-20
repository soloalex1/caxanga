using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class admCursor : MonoBehaviour
{
    public Image imagemCursor;
    public static admCursor singleton;

    public bool sobreBotao;


    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        Configuracoes.admCursor = this;
        Cursor.visible = false;
    }

    public void MudarSprite(Sprite sprite)
    {
        imagemCursor.sprite = sprite;
        imagemCursor.SetNativeSize();
        // imagemCursor.gameObject.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        imagemCursor.transform.position = Input.mousePosition;
    }
}
