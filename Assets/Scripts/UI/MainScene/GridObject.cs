using System;
using System.Collections.Generic;
using Audio;
using DefaultNamespace.Items;
using DefaultNamespace.Statics;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainScene
{
    public class GridObject:MonoBehaviour
    {
        private GridLayoutGroup _gridLayoutGroup;
        private List<GameObject> _gridObjectList;
        private BaseItem[,] _itemGrid;
        
        public bool[,] UnlockedGrid;
        
        private void Start()
        {
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            _gridObjectList = new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++)
            {
                _gridObjectList.Add(transform.GetChild(i).gameObject);
            }
            _itemGrid = new BaseItem[_gridObjectList.Count/_gridLayoutGroup.constraintCount,_gridLayoutGroup.constraintCount];
            InitUnlockedGrid();
        }

        private void InitUnlockedGrid()
        {
            UnlockedGrid=new bool[_gridObjectList.Count/_gridLayoutGroup.constraintCount,_gridLayoutGroup.constraintCount];
            for (int i = 0; i < UnlockedGrid.GetLength(0); i++)
            {
                for (int j = 0; j < UnlockedGrid.GetLength(1); j++)
                {
                    UnlockedGrid[i, j] = false;
                    _gridObjectList[i*UnlockedGrid.GetLength(1)+j].GetComponent<Image>().color = new Color(0.4f,0.4f,0.4f,1);
                    if (i >= 1 && j >= 1 && i <= 3 && j <= 3)
                    {
                        UnlockedGrid[i, j] = true;
                        _gridObjectList[i*UnlockedGrid.GetLength(1)+j].GetComponent<Image>().color = new Color(0,0,0,0);
                    }
                }
            }
        }

        private int tmpRow = -1;
        private int tmpCol = -1;
        public void GetIndex(Vector2 pos, out int row, out int col)
        {
            Vector2 relativePos=pos-(Vector2)transform.position;
            if (relativePos.x<0||relativePos.y>0||
                relativePos.x>(_gridLayoutGroup.cellSize.x+_gridLayoutGroup.spacing.x)*_gridLayoutGroup.constraintCount-_gridLayoutGroup.spacing.x||
                relativePos.y < -(_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y) * (_gridObjectList.Count / _gridLayoutGroup.constraintCount) + _gridLayoutGroup.spacing.y)
            {
                row = -1;
                col = -1;
                return;
            }

            if (relativePos.x % (_gridLayoutGroup.cellSize.x + _gridLayoutGroup.spacing.x) >
                _gridLayoutGroup.cellSize.x ||
                -relativePos.y % (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y) >
                _gridLayoutGroup.cellSize.y)
            {
                row = -1;
                col = -1;
                return;
            }
            row=(int)(-relativePos.y /(_gridLayoutGroup.cellSize.y+ _gridLayoutGroup.spacing.y));
            col=(int)(relativePos.x /(_gridLayoutGroup.cellSize.x+ _gridLayoutGroup.spacing.x));
            if (!UnlockedGrid[row, col])
            {
                row = -1;
                col = -1;
            }
        }

        public void UpdateUnlockedGrid()
        {
            for (int i = 0; i < UnlockedGrid.GetLength(0); i++)
            {
                for (int j = 0; j < UnlockedGrid.GetLength(1); j++)
                {
                    if (UnlockedGrid[i, j])
                    {
                        _gridObjectList[i*UnlockedGrid.GetLength(1)+j].GetComponent<Image>().color = new Color(0,0,0,0);
                    }
                    else
                    {
                        _gridObjectList[i*UnlockedGrid.GetLength(1)+j].GetComponent<Image>().color = new Color(0.4f,0.4f,0.4f,1);
                    }
                }
            }
        }
        public void UpdateGridView(Vector2 pos)
        {
            int row, col;
            GetIndex(pos, out row, out col);
            if (tmpRow!=-1 && tmpCol!=-1&&(tmpRow!=row||tmpCol!=col))
            {
                _gridObjectList[tmpRow*UnlockedGrid.GetLength(1)+tmpCol].GetComponent<Image>().color = new Color(0,0,0,0);
            }

            if (row != -1 && col != -1)
            {
                _gridObjectList[row * UnlockedGrid.GetLength(1) + col].GetComponent<Image>().color =
                    new Color(1, 1, 0, 0.4f);
            }
            tmpRow = row;
            tmpCol = col;
        }

        public void AddToGrid(int row, int col, DragItem dragItem,out Vector2 standardPos)
        {
            AudioManager.Instance.PlaySound("PlaceUp");
            if (_gridObjectList[row * _gridLayoutGroup.constraintCount + col].transform.childCount != 0)
            {
                Transform toBeDeleted=_gridObjectList[row * _gridLayoutGroup.constraintCount + col].transform.GetChild(0);
                _gridObjectList[row * _gridLayoutGroup.constraintCount + col].transform.DetachChildren();
                Destroy(toBeDeleted.gameObject);
            }
            dragItem.transform.SetParent(_gridObjectList[row*_gridLayoutGroup.constraintCount+col].transform);
            if (_itemGrid[row, col] != null)
            {
                AudioManager.Instance.PlaySound("Cover");
                LevelStatics.CurrentScore -= AddScoreStatics.ScoreF;
            }
            _itemGrid[row, col] = dragItem.CurrentItem;
            standardPos = transform.position+new Vector3(
                col*(_gridLayoutGroup.cellSize.x+_gridLayoutGroup.spacing.x)+_gridLayoutGroup.cellSize.x/2,
                -row*(_gridLayoutGroup.cellSize.x+_gridLayoutGroup.spacing.y)-_gridLayoutGroup.cellSize.y/2);
            _gridObjectList[row*UnlockedGrid.GetLength(1)+col].GetComponent<Image>().color = new Color(0,0,0,0);
            
        }

        public void CheckCondition(int startRow, int startCol)
        {
            BaseItem currentItem = _itemGrid[startRow, startCol];
            int currentRow = startRow;
            int currentCol = startCol;
            List<Vector2> itemVerticalByIP=new List<Vector2>();
            while (--currentRow>=0&&_itemGrid[currentRow, currentCol]!=null&&
                   _itemGrid[currentRow, currentCol].IPType == currentItem.IPType&&UnlockedGrid[currentRow, currentCol])
            {
                itemVerticalByIP.Add(new Vector2(currentRow,currentCol));
            }
            currentRow = startRow;
            while (++currentRow<(_gridObjectList.Count / _gridLayoutGroup.constraintCount)&&
                   _itemGrid[currentRow, currentCol]!=null&&
                   _itemGrid[currentRow, currentCol].IPType == currentItem.IPType&&UnlockedGrid[currentRow, currentCol])
            {
                itemVerticalByIP.Add(new Vector2(currentRow,currentCol));
            }
            
            List<Vector2> itemHorizontalByIP=new List<Vector2>();
            currentRow = startRow;
            currentCol = startCol;
            while (--currentCol>=0&&_itemGrid[currentRow, currentCol]!=null&&
                   _itemGrid[currentRow, currentCol].IPType == currentItem.IPType&&UnlockedGrid[currentRow, currentCol])
            {
                itemHorizontalByIP.Add(new Vector2(currentRow,currentCol));
            }
            currentCol = startCol;
            while (++currentCol< _gridLayoutGroup.constraintCount&&
                    _itemGrid[currentRow, currentCol]!=null &&
                    _itemGrid[currentRow, currentCol].IPType == currentItem.IPType&&UnlockedGrid[currentRow, currentCol])
            {
                itemHorizontalByIP.Add(new Vector2(currentRow,currentCol));
            }
            
            List<Vector2> itemVerticalByCraft=new List<Vector2>();
            currentRow = startRow;
            currentCol = startCol;
            while (--currentRow>=0&&_itemGrid[currentRow, currentCol]!=null&&
                   _itemGrid[currentRow, currentCol].CraftType == currentItem.CraftType&&UnlockedGrid[currentRow, currentCol])
            {
                itemVerticalByCraft.Add(new Vector2(currentRow,currentCol));
            }
            currentRow = startRow;
            while (++currentRow<(_gridObjectList.Count / _gridLayoutGroup.constraintCount)&&
                   _itemGrid[currentRow, currentCol]!=null &&
                   _itemGrid[currentRow, currentCol].CraftType == currentItem.CraftType&&UnlockedGrid[currentRow, currentCol])
            {
                itemVerticalByCraft.Add(new Vector2(currentRow,currentCol));
            }
            
            List<Vector2> itemHorizontalByCraft=new List<Vector2>();
            currentRow = startRow;
            currentCol = startCol;
            while (--currentCol>=0&&_itemGrid[currentRow, currentCol]!=null&&
                   _itemGrid[currentRow, currentCol].CraftType == currentItem.CraftType&&UnlockedGrid[currentRow, currentCol])
            {
                itemHorizontalByCraft.Add(new Vector2(currentRow,currentCol));
            }
            currentCol = startCol;
            while (++currentCol< _gridLayoutGroup.constraintCount&&
                    _itemGrid[currentRow, currentCol]!=null &&
                   _itemGrid[currentRow, currentCol].CraftType == currentItem.CraftType&&UnlockedGrid[currentRow, currentCol])
            {
                itemHorizontalByCraft.Add(new Vector2(currentRow,currentCol));
            }
            
            ChangeScore(itemVerticalByIP, itemHorizontalByIP, itemVerticalByCraft, itemHorizontalByCraft,startRow,startCol);
            ClearConditionItem(itemVerticalByIP, itemHorizontalByIP, itemVerticalByCraft, itemHorizontalByCraft,startRow,startCol);
        }

        private void ClearConditionItem(List<Vector2> itemVerticalByIP, List<Vector2> itemHorizontalByIP,
            List<Vector2> itemVerticalByCraft, List<Vector2> itemHorizontalByCraft,int row,int col)
        {
            if (itemVerticalByIP.Count < 2 && itemHorizontalByIP.Count < 2 && itemVerticalByCraft.Count < 2 &&
                itemHorizontalByCraft.Count < 2)
            {
                Debug.Log("不满足三消条件");
                return;
            }
            Debug.Log("满足三消条件！");
            AudioManager.Instance.PlaySound("Clear");

            if (itemVerticalByIP.Count >= 2)
            {
                foreach (var pos in itemVerticalByIP)
                {
                    if (_gridObjectList[(int)pos.x * UnlockedGrid.GetLength(1) + (int)pos.y].transform.childCount != 0)
                    {
                        Transform toBeDeleted = _gridObjectList[(int)pos.x * UnlockedGrid.GetLength(1) + (int)pos.y]
                            .transform.GetChild(0);
                        StartCoroutine(toBeDeleted.GetComponent<DragItem>().ClearCoroutine());
                        _itemGrid[(int)pos.x, (int)pos.y] = null;
                    }
                }
            }

            if (itemHorizontalByIP.Count >= 2)
            {
                foreach (var pos in itemHorizontalByIP)
                {
                    if (_gridObjectList[(int)pos.x * UnlockedGrid.GetLength(1) + (int)pos.y].transform.childCount != 0)
                    {
                        Transform toBeDeleted = _gridObjectList[(int)pos.x * UnlockedGrid.GetLength(1) + (int)pos.y]
                            .transform.GetChild(0);
                        StartCoroutine(toBeDeleted.GetComponent<DragItem>().ClearCoroutine());
                        _itemGrid[(int)pos.x, (int)pos.y] = null;
                    }
                }
            }

            if (itemVerticalByCraft.Count >= 2)
            {
                foreach (var pos in itemVerticalByCraft)
                {
                    if (_gridObjectList[(int)pos.x * UnlockedGrid.GetLength(1) + (int)pos.y].transform.childCount != 0)
                    {
                        Transform toBeDeleted = _gridObjectList[(int)pos.x * UnlockedGrid.GetLength(1) + (int)pos.y]
                            .transform.GetChild(0);
                        StartCoroutine(toBeDeleted.GetComponent<DragItem>().ClearCoroutine());
                        _itemGrid[(int)pos.x, (int)pos.y] = null;
                    }
                }
            }

            if (itemHorizontalByCraft.Count >= 2)
            {
                foreach (var pos in itemHorizontalByCraft)
                {
                    if (_gridObjectList[(int)pos.x * UnlockedGrid.GetLength(1) + (int)pos.y].transform.childCount != 0)
                    {
                        Transform toBeDeleted = _gridObjectList[(int)pos.x * UnlockedGrid.GetLength(1) + (int)pos.y]
                            .transform.GetChild(0);
                        StartCoroutine(toBeDeleted.GetComponent<DragItem>().ClearCoroutine());
                        _itemGrid[(int)pos.x, (int)pos.y] = null;
                    }
                }
            }
            
            _itemGrid[row, col] = null;
            Transform startContent = _gridObjectList[row * UnlockedGrid.GetLength(1) + col]
                .transform.GetChild(0);
            StartCoroutine(startContent.GetComponent<DragItem>().ClearCoroutine());
        }

        private void ChangeScore(List<Vector2> itemVerticalByIP, List<Vector2> itemHorizontalByIP,
            List<Vector2> itemVerticalByCraft, List<Vector2> itemHorizontalByCraft,int row,int col)
        {
            if (itemVerticalByIP.Count < 2 && itemHorizontalByIP.Count < 2 && itemVerticalByCraft.Count < 2 &&
                itemHorizontalByCraft.Count < 2)
            {
                return;
            }
            if (itemHorizontalByCraft.Count >= 2)
            {
                if (itemHorizontalByCraft.Count == 3)
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreB;
                }
                else if (itemHorizontalByCraft.Count == 4)
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreC;
                }
                else if(itemHorizontalByIP.Count == 2&&itemHorizontalByIP[0]==itemHorizontalByCraft[0]&&itemHorizontalByIP[1]==itemHorizontalByCraft[1])
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreE;
                }
                else if(itemHorizontalByIP.Count == 3)
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreB;
                }
                else if(itemHorizontalByIP.Count == 4)
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreC;
                }
                else if (_itemGrid[row,col].IPType!=_itemGrid[(int)itemHorizontalByCraft[0].x,(int)itemHorizontalByCraft[0].y].IPType&&
                         _itemGrid[row,col].IPType!=_itemGrid[(int)itemHorizontalByCraft[1].x,(int)itemHorizontalByCraft[1].y].IPType&&
                         _itemGrid[(int)itemHorizontalByCraft[0].x,(int)itemHorizontalByCraft[0].y].IPType!=_itemGrid[(int)itemHorizontalByCraft[1].x,(int)itemHorizontalByCraft[1].y].IPType)
                {
                    if ((int)_itemGrid[row, col].CraftType <= 1)
                    {
                        LevelStatics.CurrentScore += AddScoreStatics.ScoreD1;
                    }
                    else if ((int)_itemGrid[row, col].CraftType <= 3)
                    {
                        LevelStatics.CurrentScore += AddScoreStatics.ScoreD2;
                    }
                    else
                    {
                        LevelStatics.CurrentScore += AddScoreStatics.ScoreD3;
                    }
                }
                else
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreA;    
                }
            }
            else
            {
                if (itemHorizontalByIP.Count == 3)
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreB;
                }
                else if (itemHorizontalByIP.Count == 4)
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreC;
                }
                else if (itemHorizontalByIP.Count == 2)
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreA;    
                }
            }
            
            if (itemVerticalByCraft.Count >= 2)
            {
                if (itemVerticalByCraft.Count == 3)
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreB;
                }
                else if (itemVerticalByCraft.Count == 4)
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreC;
                }
                else if(itemVerticalByIP.Count == 2&&itemVerticalByIP[0]==itemVerticalByCraft[0]&&itemVerticalByIP[1]==itemVerticalByCraft[1])
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreE;
                }
                else if(itemVerticalByIP.Count == 3)
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreB;
                }
                else if(itemVerticalByIP.Count == 4)
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreC;
                }
                else if (_itemGrid[row,col].IPType!=_itemGrid[(int)itemVerticalByCraft[0].x,(int)itemVerticalByCraft[0].y].IPType&&
                         _itemGrid[row,col].IPType!=_itemGrid[(int)itemVerticalByCraft[1].x,(int)itemVerticalByCraft[1].y].IPType&&
                         _itemGrid[(int)itemVerticalByCraft[0].x,(int)itemVerticalByCraft[0].y].IPType!=_itemGrid[(int)itemVerticalByCraft[1].x,(int)itemVerticalByCraft[1].y].IPType)
                {
                    if ((int)_itemGrid[row, col].CraftType <= 1)
                    {
                        LevelStatics.CurrentScore += AddScoreStatics.ScoreD1;
                    }
                    else if ((int)_itemGrid[row, col].CraftType <= 3)
                    {
                        LevelStatics.CurrentScore += AddScoreStatics.ScoreD2;
                    }
                    else
                    {
                        LevelStatics.CurrentScore += AddScoreStatics.ScoreD3;
                    }
                }
                else
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreA;    
                }
            }
            else
            {
                if (itemVerticalByIP.Count == 3)
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreB;
                }
                else if (itemVerticalByIP.Count == 4)
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreC;
                }
                else if(itemVerticalByIP.Count == 2)
                {
                    LevelStatics.CurrentScore += AddScoreStatics.ScoreA;    
                }
            }
        }
    }
}