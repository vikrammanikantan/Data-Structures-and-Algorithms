using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Assignment_3
{
    public class Node
    {
        public Node(char key, Node parent)
        {
            this.key = key;
            this.parent = parent;
            this.rightSibling = null;
            leaf = false;
            linkSibling();
        }

        public Node firstChild;
        public Node rightSibling;
        public Node parent;
        public char key;
        public Boolean leaf;

        private void linkSibling()
        {
            if (parent == null) return;

            Node temp = parent.firstChild;

            if (temp == null)
            {
                parent.firstChild = this;
                return;
            }

            while (temp.rightSibling != null)
            {
                temp = temp.rightSibling;
            }

            temp.rightSibling = this;
        }
    }

    public class Trie
    {
        public Trie()
        {
            root = new Node(' ', null);
        }

        private readonly Node root;

        public Trie(String pathName)
        {
            String[] words = System.IO.File.ReadAllLines(pathName);

            for (int x = 0; x < words.Length; x++)
            {
                Add(words[x]);
            }
        }

        public void Add(String w)
        { 
            Node node = root;
            char[] word = w.ToCharArray();

            for (int x = 0; x < word.Length; x++)
            {
                if (hasChild(node, word[x]))
                {
                    node = getChild(node, word[x]);
                }
                else
                {
                    if (x == word.Length - 1)
                    {
                        node = addChild(node, word[x]);
                    }
                    else
                    {
                        node = addChild(node, word[x]);
                    }
                }
            }

            node.leaf = true;
        }

        public Boolean Contains(string w)
        {
            Node node = root;
            foreach (var t in w)
            {
                if (hasChild(node, t))
                {
                    node = getChild(node, t);
                }
                else return false;
            }

            return node.leaf;
        }

        //searches to see if a node as a child c
        private Boolean hasChild(Node parent, char c)
        {
            Node child = parent.firstChild;

            while (child != null)
            {
                if (child.key == c)
                {
                    return true;
                }
                else
                {
                    child = child.rightSibling;
                }
            }

            return false;
        }

        //returns the node of the child with character c, can't return null because hasChild has been checked
        private Node getChild(Node parent, char c)
        {
            Node child = parent.firstChild;

            while (child != null)
            {
                if (child.key == c)
                {
                    return child;
                }
                else
                {
                    child = child.rightSibling;
                }
            }

            return null;
        }

        private Node addChild(Node parent, char c)
        {

            Node child;


            if (parent.firstChild != null)
            {
                child = parent.firstChild;
            }
            else
            {
                parent.firstChild = new Node(c,parent);
                return parent.firstChild;
            }
            

            while (child.rightSibling != null)
            {
                child = child.rightSibling;
            }

            child.rightSibling = new Node(c, parent);

            return child.rightSibling;

        }
    }
}
