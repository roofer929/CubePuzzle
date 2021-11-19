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
    Vector3 destPos = new Vector3();
    Quaternion destRot = new Quaternion();

    [SerializeField] bool isMoving = false;
    [SerializeField] bool isSpinning = false;

    MeshRenderer cubeMeshRenderer;

    int colorValue = 0;



    private void Awake()
    {
        cubeMeshRenderer = realCube.GetComponent<MeshRenderer>();
    }

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
    ///<Summary>
    /// 수평 입력 체크
    ///</Summary>
    public void Dir_H()
    {
        dir.Set(Input.GetAxisRaw("Horizontal"), 0, 0);
        if (DirectionCheck(dir))
        {
            return;
        }

        MoveRotate(dir);
    }

    ///<Summary>
    /// 수직 입력 체크
    ///</Summary>
    public void Dir_V()
    {
        dir.Set(0, 0, Input.GetAxisRaw("Vertical"));
        if (DirectionCheck(dir))
        {
            return;
        }

        MoveRotate(dir);

    }

    ///<Summary>
    /// 큐브 이동과 회전을 위한 이동 값 계산
    ///</Summary>
    void MoveRotate(Vector3 dir)
    {
        destPos = transform.position + new Vector3(dir.x, 0, dir.z);
        rotDir = new Vector3(-dir.z, 0f, dir.x);
        fakeCube.RotateAround(transform.position, rotDir, spinSpeed);
        destRot = fakeCube.rotation;

        StartCoroutine(MoveCube());
        StartCoroutine(SpinCube());
    }


    ///<Summary>
    /// 큐브 이동
    ///</Summary>
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

    ///<Summary>
    /// 큐브 회전
    ///</Summary>
    IEnumerator SpinCube()
    {
        isSpinning = true;
        while (Quaternion.Angle(realCube.rotation, destRot) > 0.5f)
        {
            realCube.rotation = Quaternion.RotateTowards(realCube.rotation, destRot, spinSpeed * Time.deltaTime);
            yield return null;
        }
        isSpinning = false;
        CheckTile();
    }

    ///<Summary>
    /// 움직일 방향 이동 가능 여부 체크
    ///</Summary>
    public bool DirectionCheck(Vector3 dir)
    {
        RaycastHit hit;
        return Physics.Raycast(realCube.position, dir, out hit, 1);
    }

    ///<Summary>
    /// 타일 체크 (큐브가 이동을 완료하고 나서 실행됨)
    ///</Summary>
    public void CheckTile()
    {
        RaycastHit hit;
        if (Physics.Raycast(realCube.position, Vector3.down, out hit, 1))
        {
            if (hit.transform.CompareTag("Tile"))
            {
                var tile = hit.transform.GetComponent<Tile>();
                tile.value = colorValue;
                tile.ChangeColor(cubeMeshRenderer.material);


            }
        }
    }

    public void ChangePlayerMat(Material mat, int value)
    {
        cubeMeshRenderer.material = mat;
        colorValue = value;
    }

}
