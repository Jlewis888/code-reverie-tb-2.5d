using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursorManager : SerializedMonoBehaviour
{

    public Image cursorImage;

    public Sprite aimCursor;
    public Sprite pointerCursor;

    private void Awake()
    {
        cursorImage = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;


        // if (Application.isPlaying)
        // {
        //     Cursor.lockState = CursorLockMode.None;
        // }
        // else
        // {
        //     Cursor.lockState = CursorLockMode.Confined;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = Input.mousePosition;

        cursorImage.rectTransform.position = cursorPos;
        
        
        // if (!Application.isPlaying)
        // {
        //     return;
        // }
        //
        // Cursor.visible = false;
    }
}
