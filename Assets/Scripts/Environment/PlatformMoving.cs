using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    [SerializeField]private PointsPath _pointsPath;
    [SerializeField] private float _speed;

    private int _targetPointIndex;
    private float _timeWayToPoint;
    private float _passedTime;

    private Transform _previousPoint;
    private Transform _targetPoint;

    private void Start()
    {
        TargetNextPoint();
    }

    private void FixedUpdate()
    {
        _passedTime += Time.deltaTime;

        //������� ������ �� ���� ����� � ���������
        float passedTimePercentage = _passedTime / _timeWayToPoint;
        Debug.Log($"Changing percantage: {passedTimePercentage}");
        passedTimePercentage = Mathf.SmoothStep(0, 1, passedTimePercentage);
        //����������� ���������
        transform.position = Vector3.Lerp(_previousPoint.position, _targetPoint.position, passedTimePercentage);
        //transform.rotation = Quaternion.Lerp(_previousPoint.rotation, _targetPoint.rotation, passedTimePercentage);
        //�������� ����� ����������� - �������� ��������� � ������ � ������

        //���������� ��������� �����
        if (passedTimePercentage >= 1)
        {
            TargetNextPoint();
        }
    }   

    private void TargetNextPoint()
    {
        //��������� ����� ��� ������������ ���������
        _previousPoint = _pointsPath.PointsGet(_targetPointIndex);
        _targetPointIndex = _pointsPath.NextPointsGet(_targetPointIndex);
        _targetPoint = _pointsPath.PointsGet(_targetPointIndex);

        _passedTime = 0;//��������� ���������� �������, ����� ������� passedTimePercentage � ���� � ������ ������� ���������

        //����� ���������� �� ���������� �� ��������� �����
        float distancePoints = Vector3.Distance(_previousPoint.position, _targetPoint.position);
        _timeWayToPoint = distancePoints / _speed;
    }

    //������ ������ � ���������
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
