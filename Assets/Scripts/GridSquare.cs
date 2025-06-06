using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquare : MonoBehaviour
{
    public Image hooverImage;
    public Image activeImage;
    public Image normalImage;
    public List<Sprite> normalImages;
    private Config.SquareColor currentSquareColor = Config.SquareColor.Notset;

    public Config.SquareColor GetCurrentColor() { return currentSquareColor; }

    public bool Selected {  get;  set; }
    public int squareIndex { get; set; }
    public bool squareOccupied { get; set; }
    void Start()
    {
        Selected = false;
        squareOccupied = false;
    }
    
    public void PlaceShapeOnBoard(Config.SquareColor color)
    {
         currentSquareColor = color;
         ActivateSquare();
    }
    public void ActivateSquare()
    {
        hooverImage.gameObject.SetActive(false);
        activeImage.gameObject.SetActive(true);
        Selected = true;
        squareOccupied=true;
    }
    public void Deactivate()
    {
        currentSquareColor = Config.SquareColor.Notset;
        activeImage.gameObject.SetActive(false);
    }
    public void ClearOccupied()
    {
        Selected=false;
        squareOccupied = false;
    }
    public void SetImage(bool setFirstImage)
    {
        normalImage.GetComponent<Image>().sprite = setFirstImage? normalImages[1] : normalImages[0];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(squareOccupied == false)
        {
            Selected =true;
            hooverImage.gameObject.SetActive(true);
        }else if(collision.GetComponent<ShapeSquare>() != null)
        {
            collision.GetComponent<ShapeSquare>().SetOccupied();
        }
        
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Selected = true;
        if(squareOccupied == false)
        {
            hooverImage.gameObject.SetActive(true);
        }
        else if (collision.GetComponent<ShapeSquare>() != null)
        {
            collision.GetComponent<ShapeSquare>().SetOccupied();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(squareOccupied == false)
        {
            Selected = false;
            hooverImage.gameObject.SetActive(false);
        }else if(collision.GetComponent<ShapeSquare>()!= null)
        {
            collision.GetComponent <ShapeSquare>().UnSetOccupied();
        }
        
    }
}
