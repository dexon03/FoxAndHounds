using UnityEngine;

namespace TreeSearch
{
    public class TreeSearch
    {
        public Node Root { get; set; }

        public TreeSearch(GameObject[,] positions)
        {
            Root = new Node(positions,0,1);
            Root.CreateChildren();
        }
        
    }
}