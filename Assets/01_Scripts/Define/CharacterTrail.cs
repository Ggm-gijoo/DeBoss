using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTrail : MonoBehaviour
{
    [Range(0.1f,1f)]
    [Header("메시 생성 딜레이")]
    public float meshRefreshDelay = 0.1f;
    [Header("메시 삭제 딜레이")]
    public float meshDestroyDelay = 3f;
    [Header("메시 생성 위치")]
    public Transform instantiatePos;
    [Header("메시 셰이더")]
    public Material mat;
    [Header("메시 부모")]
    public Transform parentTrs;

    private bool isTrailActive = false;
    private SkinnedMeshRenderer[] skinnedMeshRenderers;


    public void OnTrail(float activeTime = 2f)
    {
        if (!isTrailActive)
        {
            isTrailActive = true;
            StartCoroutine(StartTrail(activeTime, 10));
        }
    }

    private IEnumerator StartTrail(float activeTime,int count)
    {
        while(activeTime > 0)
        {
            activeTime -= meshRefreshDelay;

            if (skinnedMeshRenderers == null)
                skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (var obj in skinnedMeshRenderers)
            {
                GameObject gObj = new GameObject();
                gObj.transform.SetPositionAndRotation(instantiatePos.position, instantiatePos.rotation);
                gObj.transform.SetParent(parentTrs);

                MeshRenderer renderer = gObj.AddComponent<MeshRenderer>();
                MeshFilter filter = gObj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                obj.BakeMesh(mesh);

                filter.mesh = mesh;
                renderer.material = mat;

                Destroy(gObj, meshDestroyDelay);
            }
                
            yield return new WaitForSeconds(meshRefreshDelay);
        }

        isTrailActive = false;
    }
}
