using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    //Apply the dial textures
    public Texture2D dialTex;
    public Texture2D needleTex;
    //Apply the dialPosition
    public Transform dialPos;
    //apply the angles of the dial
    public float stopAngle;
    public float topSpeedAngle;
    //Apply the topSpeed/currentSpeed
    public float topSpeed;
    public float speed;
    private PhysicsComponent physcomp;
    private void Awake()
    {
        physcomp = GameObject.FindWithTag("Player").GetComponent<PhysicsComponent>();
        
    }

    private void OnGUI()
    {
        if (physcomp != null)
        {
            GUI.DrawTexture(new Rect(dialPos.position.x, dialPos.position.y, dialTex.width, dialTex.height), dialTex);
            Vector2 centre = new Vector2(dialPos.position.x + dialTex.width / 2, dialPos.position.y + dialTex.height / 2);
            Matrix4x4 savedMatrix = GUI.matrix;
            float speedFraction = physcomp.GetVelocity().magnitude / topSpeed;
            float needleAngle = Mathf.Lerp(stopAngle, topSpeedAngle, speedFraction);
            GUIUtility.RotateAroundPivot(needleAngle, centre);
            GUI.DrawTexture(new Rect(centre.x, centre.y - needleTex.height / 2, needleTex.width, needleTex.height), needleTex);
            GUI.matrix = savedMatrix;
        }
    }
}
