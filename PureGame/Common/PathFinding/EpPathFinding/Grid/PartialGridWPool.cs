/*! 
@file PartialGridWPool.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/eppathfinding.cs>
@date July 16, 2013
@brief PartialGrid with Pool Interface
@version 2.0

@section LICENSE

The MIT License (MIT)

Copyright (c) 2013 Woong Gyu La <juhgiyo@gmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

@section DESCRIPTION

An Interface for the PartialGrid with Pool Class.

*/

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PureGame.Common.PathFinding.EpPathFinding.Grid
{
    public class PartialGridWPool : BaseGrid
    {
        private NodePool m_nodePool;

        public override int width
        {
            get
            {
                return m_gridRect.maxX - m_gridRect.minX;
            }
            protected set
            {

            }
        }

        public override int height
        {
            get
            {
                return m_gridRect.maxY - m_gridRect.minY;
            }
            protected set
            {

            }
        }


        public PartialGridWPool(NodePool iNodePool, GridRect iGridRect = null)
            : base()
        {
            if (iGridRect == null)
                m_gridRect = new GridRect();
            else
                m_gridRect = iGridRect;
            m_nodePool = iNodePool;
        }

        public PartialGridWPool(PartialGridWPool b)
            : base(b)
        {
            m_nodePool = b.m_nodePool;
        }
       
        public void SetGridRect(GridRect iGridRect)
        {
            m_gridRect = iGridRect;
        }


        public bool IsInside(int iX, int iY)
        {
            if (iX < m_gridRect.minX || iX > m_gridRect.maxX || iY < m_gridRect.minY || iY > m_gridRect.maxY)
                return false;
            return true;
        }

        public override Node GetNodeAt(int iX, int iY)
        {
            var pos = new Point(iX, iY);
            return GetNodeAt(pos);
        }

        public override bool IsWalkableAt(int iX, int iY)
        {
            var pos = new Point(iX, iY);
            return IsWalkableAt(pos);
        }

        public override bool SetWalkableAt(int iX, int iY, bool iWalkable)
        {
            if (!IsInside(iX,iY))
                return false;
            Point pos = new Point(iX, iY);
            m_nodePool.SetNode(pos, iWalkable);
            return true;
        }

        public bool IsInside(Point iPos)
        {
            return IsInside(iPos.X, iPos.Y);
        }

        public override Node GetNodeAt(Point iPos)
        {
            if (!IsInside(iPos))
                return null;
            return m_nodePool.GetNode(iPos);
        }

        public override bool IsWalkableAt(Point iPos)
        {
            if (!IsInside(iPos))
                return false;
            return m_nodePool.Nodes.ContainsKey(iPos);
        }

        public override bool SetWalkableAt(Point iPos, bool iWalkable)
        {
            return SetWalkableAt(iPos.X, iPos.Y, iWalkable);
        }

        public override void Reset()
        {
            int rectCount=(m_gridRect.maxX-m_gridRect.minX) * (m_gridRect.maxY-m_gridRect.minY);
            if (m_nodePool.Nodes.Count > rectCount)
            {
                var travPos = new Point(0, 0);
                for (var xTrav = m_gridRect.minX; xTrav <= m_gridRect.maxX; xTrav++)
                {
                    travPos.X = xTrav;
                    for (int yTrav = m_gridRect.minY; yTrav <= m_gridRect.maxY; yTrav++)
                    {
                        travPos.Y = yTrav;
                        var curNode=m_nodePool.GetNode(travPos);
                        if (curNode!=null)
                            curNode.Reset();
                    }
                }
            }
            else
            {
                foreach (KeyValuePair<Point, Node> keyValue in m_nodePool.Nodes)
                {
                    keyValue.Value.Reset();
                }
            }
        }


        public override BaseGrid Clone()
        {
            PartialGridWPool tNewGrid = new PartialGridWPool(m_nodePool,m_gridRect);
            return tNewGrid;
        }
    }

}