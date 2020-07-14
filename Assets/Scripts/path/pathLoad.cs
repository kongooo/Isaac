using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathLoad : MonoBehaviour {

    private GRID Grid;

    void Start()
    {
        Grid = GetComponent<GRID>();
    }
    
    void Update()
    {
        if (Vector2.Distance(Camera.main.transform.position, transform.position) <= 7)
            changepath(Grid.StartTransform.position, Grid.EndTransform.position);
    }

    public void changepath(Vector3 s, Vector3 e)
    {
        GRID.NodeItem StartNode = Grid.GetGrid(s);     //将开始和结束的坐标转换成节点坐标
        GRID.NodeItem EndNode = Grid.GetGrid(e);

        //定义开启列表和关闭列表
        //开启列表：存储可以成为下一个父节点的节点
        //关闭列表：存储成为过父节点的节点
        List<GRID.NodeItem> OpenList = new List<GRID.NodeItem>();
        HashSet<GRID.NodeItem> CloseList = new HashSet<GRID.NodeItem>();

        OpenList.Add(StartNode);

        while (OpenList.Count > 0)
        {
            //将开启列表的第一个节点记录下来
            GRID.NodeItem ParentNode = OpenList[0];

            //遍历开启列表寻找parent
            for (int i = 0; i < OpenList.Count; i++)
            {
                //若开启列表中的节点的总花费更小或不变并且到终点的花费更小
                if (OpenList[i].fCost <= ParentNode.fCost && OpenList[i].gCost < ParentNode.gCost)
                {
                    ParentNode = OpenList[i];
                }
            }

            //将找到的父节点从开启列表中删除，加入关闭列表
            OpenList.Remove(ParentNode);
            CloseList.Add(ParentNode);

            //若父节点为终点节点则停止更新路程花费并且生成路径
            if (ParentNode == EndNode)
            {
                //生成路径
                ShowPath(StartNode, EndNode);
                return;
            }

            // 判断周围节点，选择一个最优的节点
            foreach (var item in Grid.GetNearNode(ParentNode))
            {
                // 如果是墙或者已经在关闭列表中
                if (item.isWall || CloseList.Contains(item))
                    continue;
                // 计算当前相领节点现开始节点距离
                int newCost = ParentNode.gCost + updatecost(ParentNode, item);
                // 如果距离更小，或者原来不在开始列表中
                if (newCost < item.gCost || !OpenList.Contains(item))
                {
                    // 更新与开始节点的距离
                    item.gCost = newCost;
                    // 更新与终点的距离
                    item.hCost = updatecost(item, EndNode);
                    // 更新父节点为当前选定的节点
                    item.parent = ParentNode;
                    // 如果节点是新加入的，将它加入打开列表中
                    if (!OpenList.Contains(item))
                    {
                        OpenList.Add(item);
                    }
                }
            }
        }
        ShowPath(StartNode, null);
    }

    //生成路径为从结束节点处开始依次寻找其父节点直到开始节点处停止
    void ShowPath(GRID.NodeItem startnode, GRID.NodeItem endnode)
    {
        //定义存储路径的列表
        List<GRID.NodeItem> path = new List<GRID.NodeItem>();
        //当结束节点不为空时

        if (endnode != null)
        {
            //存储父节点的变量
            GRID.NodeItem temp = endnode;
            //当没有达到开始节点时
            while (temp != startnode)
            {
                //向存储路径的列表中添加父节点
                path.Add(temp);
                //更新父节点
                temp = temp.parent;
            }
            //反转路径（之前是一条从结束节点到开始节点的路）
            path.Reverse();
        }

        Grid.upadatePath(path);
    }

    int updatecost(GRID.NodeItem s, GRID.NodeItem e)
    {
        int x = Mathf.Abs(s.x - e.x);
        int y = Mathf.Abs(s.y - e.y);
        if (x > y)
        {
            return y * 14 + (x - y) * 10;
        }
        else
            return x * 14 + (y - x) * 10;
    }
}
