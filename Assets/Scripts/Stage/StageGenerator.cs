using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator
{
    private List<RoomNode> allSpaceNodes = new List<RoomNode>();
    private int _stageWidth;
    private int _stageLength;

    public StageGenerator(int stageWidth, int stageLength)
    {
        this._stageWidth = stageWidth;
        this._stageLength = stageLength;
    }

    public List<Node> CalculateRooms(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(_stageWidth, _stageLength);
        allSpaceNodes = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);
        List<Node> roomSpaces = StructureHelper.TraverseGraphToExtractLowestLeafs(bsp.RootNode);
        
        RoomGenerator roomGenerator = new RoomGenerator(maxIterations, roomLengthMin, roomWidthMin);
        List<RoomNode> roomList = roomGenerator.GenerateRoomsInGivenSpaces(roomSpaces);
        return new List<Node>(roomList);
    }
}
