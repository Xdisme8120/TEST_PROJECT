using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class BagGrid : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    //物品ID
    Item item;
    //物品数量
    int goodCount;

    private RectTransform canvas;          //得到canvas的ugui坐标
    private RectTransform imgRect;        //得到图片的ugui坐标
    Vector2 offset = new Vector3();
    Image image;


    Text textCount;

    Transform temp;
    GameObject tempGame;

    private void Awake()
    {
        textCount = GetComponentInChildren<Text>();
        image = transform.GetComponent<Image>();
        canvas = transform.root.GetComponent<RectTransform>();
        imgRect = transform.GetComponent<RectTransform>();
    }

    //拖拽中
    public void OnDrag(PointerEventData eventData)
    {

        Debug.Log(item.ID);
        if (item.ID == -1)
            return;

        tempGame = eventData.pointerCurrentRaycast.gameObject;
        Debug.Log(tempGame);

        Vector2 mouseDrag = eventData.position;   //当鼠标拖动时的屏幕坐标
        Vector2 uguiPos = new Vector2();   //用来接收转换后的拖动坐标
                                           //和上面类似
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDrag, eventData.enterEventCamera, out uguiPos);

        if (isRect)
        {
            //SetDragRange();
            //设置图片的ugui坐标与鼠标的ugui坐标保持不变
            imgRect.anchoredPosition = offset + uguiPos;
        }
    }
    //鼠标按下
    public void OnPointerDown(PointerEventData eventData)
    {


        temp = transform.parent;
        image.raycastTarget = false;
        transform.SetParent(transform.parent.parent);
        transform.localScale = Vector3.one;
        Vector2 mouseDown = eventData.position;    //记录鼠标按下时的屏幕坐标
        Vector2 mouseUguiPos = new Vector2();   //定义一个接收返回的ugui坐标
                                                //RectTransformUtility.ScreenPointToLocalPointInRectangle()：把屏幕坐标转化成ugui坐标
                                                //canvas：坐标要转换到哪一个物体上，这里img父类是Canvas，我们就用Canvas
                                                //eventData.enterEventCamera：这个事件是由哪个摄像机执行的
                                                //out mouseUguiPos：返回转换后的ugui坐标
                                                //isRect：方法返回一个bool值，判断鼠标按下的点是否在要转换的物体上
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDown, eventData.enterEventCamera, out mouseUguiPos);
        if (isRect)   //如果在
        {
            //计算图片中心和鼠标点的差值
            offset = imgRect.anchoredPosition - mouseUguiPos;
        }
    }
    //鼠标抬起
    public void OnPointerUp(PointerEventData eventData)
    {
        if (tempGame == null)
        {
            Debug.Log("1111");
            transform.SetParent(temp);
            transform.localPosition = new Vector2(90, -90);
            image.raycastTarget = true;
            offset = Vector2.zero;
            return;
        }

        //如果检测到名字是饰品
        if (tempGame.tag == "Item" || tempGame.name == "trinket")
        {
            transform.SetParent(tempGame.transform.parent);
            tempGame.transform.SetParent(temp);
            tempGame.transform.localPosition = new Vector2(70, -70);
            transform.localPosition = new Vector2(70, -70);
            image.raycastTarget = true;
            offset = Vector2.zero;
            return;
        }
        //如果检测到名字是武器
        if (tempGame.tag == "Item" || tempGame.name == "weapon")
        {

        }
        //如果检测到名字是护甲
        if (tempGame.tag == "Item" || tempGame.name == "armor")
        {

        }

        if (tempGame.tag == "Item"|| tempGame.name== "Item")
        {
            
            transform.SetParent(tempGame.transform.parent);
            tempGame.transform.SetParent(temp);
            tempGame.transform.localPosition = new Vector2(90, -90);
            transform.localPosition = new Vector2(90, -90);
            image.raycastTarget = true;
            offset = Vector2.zero;
        }
        else
        {
            Debug.Log("0000");
            transform.SetParent(temp);
            transform.localPosition = new Vector2(90, -90);
            image.raycastTarget = true;
            offset = Vector2.zero;
        }
    }

    //设置物品ID,并通过ID拿到相应的图片
    //设置物品数量,并设置Text
    public void SetGoodInfo(int ID, int Count)
    {
        this.item.ID = ID;
        this.goodCount = Count;
        textCount.text = goodCount.ToString();
        image.sprite = Resources.Load<Sprite>("Item/" + item.ID.ToString());
    }
}
