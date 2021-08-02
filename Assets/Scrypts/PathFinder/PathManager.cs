using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrypts.Entity
{
    [Serializable]
    public class PathManager
    {
        private Vector2[] purposes;
        private List<Node> nodes = new List<Node>();

        public PathManager(Vector2[] points)
        {
            GameObject[] fortresses = GameObject.FindGameObjectsWithTag("Fortress");
            purposes = new Vector2[fortresses.Length];
            for (int i = 0; i < purposes.Length; i++)
                purposes[i] = fortresses[i].transform.position;
            CreateNodeList(points);
        }

        public Vector2[] GetFortress() => purposes;

        private void CreateNodeList(Vector2[] shelters)
        {
            List<Tuple<Vector2, Vector2, float>> PositionPair = new List<Tuple<Vector2, Vector2, float>>();
            //foreach (Vector2 shelter in Shelters)
            //{
            //    List<Tuple<Vector2, float>> Distances = new List<Tuple<Vector2, float>>();
            //    foreach (Vector2 purpose in Purposes)
            //        Distances.Add(new Tuple<Vector2, float>(purpose, Mathf.Sqrt((purpose - shelter).sqrMagnitude)));
            //    Distances = ShellSort(Distances);
                
            //    if (Distances.Count > 1)
            //        Distances.RemoveRange(1, Distances.Count - 1);
            //    PositionPair.Add(new Tuple<Vector2, Vector2, float>(shelter, Distances[0].Item1, Distances[0].Item2));
            //}

            foreach (Vector2 currentshelter in shelters)
            {
                List<Tuple<Vector2, float>> directions = new List<Tuple<Vector2, float>>();
                foreach (Vector2 shelter in shelters)
                {
                    List<Tuple<Vector2, float>> Positions = new List<Tuple<Vector2, float>>();
                    foreach (Vector2 purpose in purposes)
                        Positions.Add(new Tuple<Vector2, float>(purpose - shelter, (purpose - shelter).magnitude));

                    Positions = ShellSort(Positions);
                    Positions.RemoveRange(0, Positions.Count - 1);
                    directions.Add(new Tuple<Vector2, float>(shelter, Vector2.Angle(currentshelter - shelter, Positions[0].Item1)));
                }
                directions = ShellSort(directions);
                directions.RemoveRange(0, directions.Count - 1);

                float length, angle;
                //length = (purposes[0] - shelters[0]).magnitude;
                //Tuple<Vector2, float> preferendPoint = new Tuple<Vector2, float>(shelters[0], length);
                //foreach(Vector2 shelter in shelters)
                //{
                //    foreach(Vector2 purpose in purposes)
                //    {
                //        length = (currentshelter - shelter).magnitude;
                //        angle = Vector2.Angle(currentshelter - shelter, purpose - shelter);
                //        if (length < preferendPoint.Item2 && angle > 90)
                //            preferendPoint = new Tuple<Vector2, float>(shelter, length);
                //    }    
                //}

                if (directions[0].Item2 > 90)
                    nodes.Add(new Node(currentshelter, directions[0].Item1));
                else
                    nodes.Add(new Node(currentshelter, ClosestFortress(currentshelter)));

                //foreach (Tuple<Vector2, Vector2, float> pair in PositionPair)
                //    if (pair.Item1 == currentshelter)
                //        nodes.Add(new Node(currentshelter, pair.Item2));

            }

            foreach (Node curentnode in nodes)
                foreach (Node node in nodes)
                    if (curentnode.EndPoint == node.StartPoint)
                        curentnode.SetNextNode(node);

            foreach (Node node in nodes)
            {
                Node nodew = node;
                while (nodew.NextNode != null)
                    nodew = nodew.NextNode;
            }
        }

        public Vector2 ClosestFortress(Vector2 position)
        {
            float length = (purposes[0] - position).magnitude;
            Tuple<Vector2, float> preferTarget = new Tuple<Vector2, float>(purposes[0], length);
            for (int i = 1; i < purposes.Length; i++)
            {
                length = (purposes[i] - position).magnitude;
                if (length < preferTarget.Item2)
                    preferTarget = new Tuple<Vector2, float>(purposes[i], length);
            }
            return preferTarget.Item1;
        }

        public Vector2[] SearchPath(Vector2 EntityPosition)
        {
            List<Vector2> path = new List<Vector2>();
            List<Tuple<Vector2, float>> pos = new List<Tuple<Vector2, float>>();

            foreach (Node node in nodes)
                pos.Add(new Tuple<Vector2, float>(node.StartPoint, Mathf.Sqrt((-node.StartPoint + EntityPosition).sqrMagnitude)));

            pos = ShellSort(pos);

            if (pos.Count > 1)
                pos.RemoveRange(1, pos.Count - 1);

            Node StartNode = null;

            foreach (Node node in nodes)
                if (node.StartPoint == pos[0].Item1)
                {
                    StartNode = node;
                    break;
                }

            if (StartNode.StartPoint != null)
                path.Add(StartNode.StartPoint);
            else
            {
                Vector2 point = ClosestFortress(EntityPosition);
                return new Vector2[] { point };
            }

            while (StartNode.NextNode != null)
            {
                path.Add(StartNode.NextNode.StartPoint);
                StartNode = StartNode.NextNode;
            }

            path.Add(StartNode.EndPoint);

            return path.ToArray();
        }


        private List<Tuple<Vector2, float>> ShellSort(List<Tuple<Vector2, float>> list)
        {
            int len = list.Count / 2;
            while (len >= 1)
            {
                for (int i = len; i < list.Count; i++)
                {
                    int j = i;
                    while ((j >= len) && (list[j - len].Item2 > list[j].Item2))
                    {
                        var t = list[j];
                        list[j] = list[j - len];
                        list[j - len] = t;
                        j = j - len;
                    }
                }

                len = len / 2;
            }
            return list;
        }

    }

}

