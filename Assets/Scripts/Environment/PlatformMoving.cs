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

        //сколько прошли до след точки в процентах
        float passedTimePercentage = _passedTime / _timeWayToPoint;
        Debug.Log($"Changing percantage: {passedTimePercentage}");
        passedTimePercentage = Mathf.SmoothStep(0, 1, passedTimePercentage);
        //передвигаем платформу
        transform.position = Vector3.Lerp(_previousPoint.position, _targetPoint.position, passedTimePercentage);
        //transform.rotation = Quaternion.Lerp(_previousPoint.rotation, _targetPoint.rotation, passedTimePercentage);
        //возможно потом понадобится - вращение платформы и игрока к точкам

        //возвращаем платформу назад
        if (passedTimePercentage >= 1)
        {
            TargetNextPoint();
        }
    }   

    private void TargetNextPoint()
    {
        //установка точек для передвижения платформы
        _previousPoint = _pointsPath.PointsGet(_targetPointIndex);
        _targetPointIndex = _pointsPath.NextPointsGet(_targetPointIndex);
        _targetPoint = _pointsPath.PointsGet(_targetPointIndex);

        _passedTime = 0;//обнуление прошедшего времени, чтобы вернуть passedTimePercentage к нулю и начать возврат платформы

        //берем расстояние от предыдущей до следующей точки
        float distancePoints = Vector3.Distance(_previousPoint.position, _targetPoint.position);
        _timeWayToPoint = distancePoints / _speed;
    }

    //захват игрока к платформе
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
