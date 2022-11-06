using System;

[Serializable]
public struct BlockSideParameters
{
    public bool IsLeftSide;
    public bool IsRightSide;
    public bool IsFrontSide;
    public bool IsBackSide;
    public bool IsTopSide;
    public bool IsBottomSide;

    public void SetAllSideAsActive()
    {
        IsLeftSide = true;
        IsRightSide = true;
        IsFrontSide = true;;
        IsBackSide = true;;
        IsTopSide = true;;
        IsBottomSide = true;;
    }
}