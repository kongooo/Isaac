using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRID : MonoBehaviour {

    public GameObject node;       //ai的路标
    public GameObject enemy;
    public Transform StartTransform;
    [HideInInspector] public Transform EndTransform;
    public LayerMask floorLayer;
    public float speed;

    private float NodeRadius=0.4f;        //节点半径

    public class NodeItem              //节点类 储存每一个节点的信息
    {
        public bool isWall;
        public int x, y;          //节点坐标
        public Vector3 pos;       //位置
        public int gCost;         //起点花费
        public int hCost;         //终点花费
        public int fCost          //总花费
        {
            get
            {
                return gCost + hCost;
            }
        }
        public NodeItem parent;    //父节点（中心节点）
        public NodeItem(bool Iswall, int X, int Y, Vector3 Pos)    //初始化
        {
            this.x = X;
            this.y = Y;
            this.isWall = Iswall;
            this.pos = Pos;
        }
    }

    private NodeItem[,] Grid;    //二维数组

    public int centerX , centerY;
    private int Width=18, Height=10;
    private int width=7, height=4;
    private int m, n;

    //在Hierarchy中管理生成路标的物体
    private GameObject Path;

    public List<GameObject> PathObject = new List<GameObject>();

    void Start()
    {
        EndTransform = GameObject.FindGameObjectWithTag("player").transform;
        Grid = new NodeItem[width*2+1,height*2+1];                           //创建对应的节点二维数组
        Path = new GameObject("PathRange");
        m = Mathf.Abs(centerX / Width) ;
        n =Mathf.Abs(centerY / Height) ;
    }

    void Update()
    {
        if (Vector2.Distance(Camera.main.transform.position, transform.position) <= 7)
        {
            for (int x = centerX - width; x < centerX + width; x++)
            for (int y = centerY - height; y < centerY + height; y++)
            {
                Vector3 pos = new Vector3(x, y, 0);
                bool isWall = false;
                Collider[] colliders = Physics.OverlapSphere(pos, NodeRadius, floorLayer);
                if (colliders.Length > 0)
                {
                    isWall = true;
                        Debug.Log("iswall");
                }
                //Debug.Log(gameObject.name+ new Vector2(Mathf.Abs(x) - Width * m - 2, Mathf.Abs(y) - Height * n - 1));
                Grid[Mathf.Abs(x) - Width * m - 2, Mathf.Abs(y) - Height * n - 1] = new NodeItem(isWall, x, y, pos);      //构建节点
            }
        }           
    }

    //根据坐标位置获得对应的节点
    public NodeItem GetGrid(Vector3 position)
    {
        //取得坐标的近似整数作为节点的序列
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);
        //限定x y范围
        x = Mathf.Clamp(x, centerX-width, centerX + width);
        y = Mathf.Clamp(y, centerY-height,centerY+height);
        return Grid[Mathf.Abs(x) - Width * m - 2, Mathf.Abs(y) - Height * n - 1];
    }


    //得到周边节点
    public List<NodeItem> GetNearNode(NodeItem node)
    {
        List<NodeItem> NearList = new List<NodeItem>();
        for (int i = -1; i < 2; i++)
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0)                 //跳过自身
                    continue;
                else
                {
                    int x = node.x + i;
                    int y = node.y + j;
                    if (x > centerX-width && x < centerX+width && y > centerY-height && y < centerY+height) //不超过边界则放到集合中
                    {
                        NearList.Add(Grid[Mathf.Abs(x) - Width * m - 2, Mathf.Abs(y) - Height * n - 1]);
                    }
                }
            }
        return NearList;
    }

    //更新路标
    public void upadatePath(List<NodeItem> path)
    {
        //得到场景中的路标的长度
        int agolength = PathObject.Count;
        //遍历新生成的路需要的节点
        for (int i = 0, max = path.Count; i < max; i++)
        {
            //当节点序号小于路标物体的数量时，设置路标物体的位置并激活路标物体
            if (i < agolength)
            {
                PathObject[i].transform.position = path[i].pos;
                PathObject[i].SetActive(true);
            }
            else   //实例化路标物体补充新生成的路
            {
                GameObject obj = GameObject.Instantiate(node, path[i].pos, Quaternion.identity) as GameObject;
                obj.transform.SetParent(Path.transform);
                PathObject.Add(obj);
            }
        }
        //把不需要的路标setfalse
        for (int i = path.Count; i < PathObject.Count; i++)
        {
            PathObject[i].SetActive(false);
        }
        chase(enemy, PathObject, path.Count);
    }

    public void chase(GameObject AI, List<GameObject> paths, int length)
    {
        bool ischase=true;
        Vector3[] pathpos = new Vector3[paths.Count];
        for (int j = 0; j < paths.Count; j++)
        {
            if (paths[j].activeSelf)
                ischase = false;
        }
        if (ischase)
        {
            AI.GetComponent<Rigidbody>().MovePosition(Vector2.Lerp(transform.position, GameObject.FindGameObjectWithTag("player").transform.position, speed * Time.deltaTime*0.3f));
            Debug.Log("walk");
        }
            
        else
        {
            int currentpoint = 0;
            for (int i = 0; i < length; i++)
            {
                pathpos[i] = paths[i].transform.position;
            }
            Vector3 AIpos = AI.GetComponent<Transform>().position;
            if (AIpos != pathpos[currentpoint])
            {
                AI.GetComponent<Rigidbody>().MovePosition(Vector2.Lerp(AIpos, pathpos[currentpoint], speed * Time.deltaTime));
            }
            else
                currentpoint = (currentpoint + 1) % pathpos.Length;

            if (gameObject.name != "fly" && gameObject.name != "spider")
            {
                //动画判断
                Vector3 dir = pathpos[currentpoint] - AIpos;

                AI.GetComponent<Animator>().SetFloat("lf", dir.x);

                AI.GetComponent<Animator>().SetFloat("ud", dir.y);
            }
        }
        
        
    }
}
