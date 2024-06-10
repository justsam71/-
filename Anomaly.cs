using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ����� ���������, ����� � AnomalyManager �� ��������� ������ Serializable ����� � �� ������������ ��������� if-statement ��� �������
 * ������ � ����. 
 */

public abstract class Anomaly : MonoBehaviour
{
    //� ���� ����� ���� �� �������, �.�. � �� ����� ������ �� � Unity ���������. �� ��� �� ������� ������ ���� ����� ����� �� �����������.
    public virtual void StartAnomaly() {}

    public virtual void firstAnomaly() {}

    public virtual void secondAnomaly() {}
    public virtual void thirdAnomaly() {}

    public virtual void fourthAnomaly() {}

    public virtual void fifthAnomaly() {}

    public virtual void FinalAnomaly() {}

    public virtual void End() {}
}
