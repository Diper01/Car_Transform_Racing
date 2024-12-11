using UnityEngine;
using System.Collections;

public class PlayerWheelsRotation : MonoBehaviour
{
    public CarMove runCar;


	[SerializeField]
	private Transform[] wheels;
	[Range(0,1)]
    public float rotateYPercent = 1;
    public float rotateDeltaY = 10;
    public float rotateDeltaX = 100;

    private float rotateX, rotateY = 0;

    private Vector3 vector = new Vector3();
	void Start()
    {

		runCar = GetComponentInParent<CarMove>();

		wheels = new Transform[transform.childCount];
		for (int i = 0; i < wheels.Length; i++)
		{
			wheels[i] = transform.GetChild(i);
		}

	}

	void FixedUpdate()
	{
		if (runCar == null || GameData.Instance.IsPause)
			return;

		rotateX += runCar.speed * rotateDeltaX * Time.fixedDeltaTime;
		if (rotateX >= 360)
			rotateX -= 360;

		rotateY = Mathf.Lerp(0, runCar.yMaxRotate * rotateYPercent, Time.fixedDeltaTime * rotateDeltaY);
		vector.x = rotateX;
		vector.y = rotateY;
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].localEulerAngles = vector;
        }
	}
}
