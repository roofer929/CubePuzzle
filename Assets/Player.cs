using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform fakeCube = null;
    [SerializeField] private Transform realCube = null;

    [SerializeField] private float spinSpeed = 270;
    [SerializeField] float moveSpeed = 3f;
    Vector3 dir = new Vector3();
    Vector3 rotDir = new Vector3();
    Quaternion destRot = new Quaternion();

    [SerializeField] bool isMoving = false;
    [SerializeField] bool isSpinning = false;



    Vector3 destPos;

    private void Update()
    {
        if (isSpinning || isMoving)
            return;

        if (Input.GetButtonDown("Horizontal"))
        {
            Dir_H();
        }
        else if (Input.GetButtonDown("Vertical"))
        {
            Dir_V();
        }
    }

    public void Dir_H()
    {
        dir.Set(Input.GetAxisRaw("Horizontal"), 0, 0);
        if (DirectionCheck(dir))
        {
            return;
        }

        MoveRotate(dir);
    }

    public void Dir_V()
    {
        dir.Set(0, 0, Input.GetAxisRaw("Vertical"));
        if (DirectionCheck(dir))
        {
            return;
        }

        MoveRotate(dir);

    }

    void MoveRotate(Vector3 dir)
    {
        destPos = transform.position + new Vector3(dir.x, 0, dir.z);
        rotDir = new Vector3(-dir.z, 0f, dir.x);
        fakeCube.RotateAround(transform.position, rotDir, spinSpeed);
        destRot = fakeCube.rotation;

        StartCoroutine(MoveCube());
        StartCoroutine(SpinCube());
    }


    IEnumerator MoveCube()
    {
        isMoving = true;
        while (Vector3.SqrMagnitude(transform.position - destPos) >= 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = destPos;
        isMoving = false;
    }

    IEnumerator SpinCube()
    {
        isSpinning = true;
        while (Quaternion.Angle(realCube.rotation, destRot) > 0.5f)
        {
            realCube.rotation = Quaternion.RotateTowards(realCube.rotation, destRot, spinSpeed * Time.deltaTime);
            yield return null;
        }
        isSpinning = false;
    }


    public bool DirectionCheck(Vector3 dir)
    {
        RaycastHit hit;
        return Physics.Raycast(realCube.position, dir, out hit, 1);
    }

}
