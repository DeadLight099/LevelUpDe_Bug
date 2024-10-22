using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPath : MonoBehaviour
{
    public Transform PointsGet(int pointIndex)
    {
        return transform.GetChild(pointIndex);//����� �������� ����� 
    }

    public int NextPointsGet(int currentPoint)
    {
        int nextPoint = currentPoint + 1;
        
        if(nextPoint == transform.childCount)
        {
            nextPoint = 0;
        }
        return nextPoint;//������������� ��������� ����� ��� ������������ � ���
    }
}
