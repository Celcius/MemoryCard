using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundController : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup _grid;

    private const float CARD_WIDTH_RATIO = 4.5f / 3.5f;
    void Start()
    {
        
        Card[] cards = GameManager.Instance.StartGame();

        float totalSpacing = _grid.spacing.x * (cards.Length-1);

        float rowCardSize = Mathf.Max(1, (Screen.width - totalSpacing) / (GameManager.Instance.Rows));
        Vector2 cellSize = new Vector2(rowCardSize,
                                       rowCardSize*  CARD_WIDTH_RATIO);
        _grid.cellSize = cellSize;
        foreach(Card c in cards)
        {
            c.transform.SetParent(_grid.transform,true);
        }
        _grid.CalculateLayoutInputHorizontal();

        _grid.enabled = true;
        StartCoroutine(DisableGrid());
    }

    private IEnumerator DisableGrid()
    {
        yield return new WaitForSeconds(1);
        _grid.enabled = false; 
    }
}
