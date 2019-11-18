using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class BagGrid : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Item GetItem
    {
        get{return item;}
    }
    public int GetCount
    {
        get{return goodCount;}
    }
    //物品ID
    Item item = new Item();
    //物品数量
    int goodCount;

    private RectTransform canvas;          //得到canvas的ugui坐标
    private RectTransform imgRect;        //得到图片的ugui坐标
    Vector2 offset = new Vector3();
    Image image;

    Vector3 tempPos;
    Text textCount;

    public Transform temp;
    GameObject tempGame;

    private void Awake()
    {
        textCount = GetComponentInChildren<Text>();
        image = transform.GetComponent<Image>();
        canvas = transform.root.GetComponent<RectTransform>();
        imgRect = transform.GetComponent<RectTransform>();
        item.ID = -1;
    }

    //拖拽中
    public void OnDrag(PointerEventData eventData)
    {
        if (item.ID == -1)
            return;

        tempGame = eventData.pointerCurrentRaycast.gameObject;
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
        transform.SetParent(transform.parent.parent.parent.parent);
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
            NoExchange();
            return;
        }
        if (tempGame == null && tempGame.name == "Slot")
        {
            NoExchange();
            return;
        }

        if (item.ItemType == 2)
        {
            //如果检测到名字是饰品
            if (tempGame.tag == "Item" && tempGame.transform.parent.name == "trinket"&&item.UseType==1)
            {
                Exchange();
                Debug.Log("trinket");
                return;
            }
            //如果检测到名字是武器
            if (tempGame.tag == "Item" && tempGame.transform.parent.name == "weapon" && item.UseType == 2)
            {
                Exchange();
                Debug.Log("weapon");
                return;

            }
            //如果检测到名字是护甲
            if (tempGame.tag == "Item" && tempGame.transform.parent.name == "armor" && item.UseType == 3)
            {
                Exchange();
                Debug.Log("armor");
                return;

            }
        }
        if (tempGame.tag == "Item" && tempGame.transform.parent.name == "Slot")
        {
            Exchange();
        }
        else
        {
            NoExchange();
        }
    }

    //设置物品ID,并通过ID拿到相应的图片
    //设置物品数量,并设置Text
    public void SetGoodInfo(Item item, int Count)
    {
        this.item = item;
        this.goodCount = Count;
        textCount.text = goodCount.ToString();
        image.sprite = Resources.Load<Sprite>("Item/" + item.ID.ToString());
    }
    //重置父对象
    public void SetTemp()
    {
        temp = transform.parent;
    }
    //格子内的物品交换方法
    void Exchange()
    {
        transform.SetParent(tempGame.transform.parent);
        tempGame.transform.SetParent(temp);
        tempGame.GetComponent<BagGrid>().SetTemp();
        tempGame.transform.localPosition = new Vector2(50, -50);
        tempGame.transform.localScale = Vector3.one;
        transform.localPosition = new Vector2(50, -50);
        transform.localScale = Vector3.one;
        image.raycastTarget = true;
        offset = Vector2.zero;
        SetTemp();
        tempGame = null;
        tempPos = transform.position;
    }
    //格子内物品不交换返回原有格子
    void NoExchange()
    {
        transform.SetParent(temp);
        transform.localPosition = new Vector2(50, -50);
        transform.localScale = Vector3.one;
        image.raycastTarget = true;
        offset = Vector2.zero;
    }
}
