using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class admCursor : MonoBehaviour
{
    public Sprite spriteCursorAtual;

    public Image imagemCursor;
    public static admCursor singleton;


    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        Configuracoes.admCursor = this;
        Cursor.visible = false;
        // imagemCursor.gameObject.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
    }

    public void MudarSprite(Sprite sprite)
    {
        imagemCursor.sprite = sprite;
        imagemCursor.SetNativeSize();
    }
    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        imagemCursor.transform.position = Input.mousePosition;
    }
}
