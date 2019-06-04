using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMeter : MonoBehaviour
{
    //Apply the dial textures
    [SerializeField] private Texture2D dialTex;
    [SerializeField] private Texture2D needleTex;
    //Apply the dialPosition
    [SerializeField] private RectTransform dialPos;
    //apply the angles of the dial
    [SerializeField] private float stopAngle;
    [SerializeField] private float topSpeedAngle;
    //Apply the topSpeed/currentSpeed
    [SerializeField] private float topSpeed;
    private float speed;
    private PhysicsComponent physcomp;
    private void Awake()
    {
        physcomp = GameObject.FindWithTag("Player").GetComponent<PhysicsComponent>();
        
    }

    private void OnGUI()
    {
        if (physcomp != null)
        {
            Vector3 playerSpeed = new Vector3(physcomp.GetVelocity().x, 0f, physcomp.GetVelocity().z);
            playerSpeed *= physcomp.GetSpeedIncrease();
            topSpeed *= physcomp.GetSpeedIncrease();
            speed = playerSpeed.magnitude;
            GUI.DrawTexture(new Rect(dialPos.position.x, dialPos.position.y, dialTex.width, dialTex.height), dialTex);
            Vector2 centre = new Vector2(dialPos.position.x + dialTex.width / 2, dialPos.position.y + dialTex.height / 2);
            Matrix4x4 savedMatrix = GUI.matrix;
            float speedFraction = (speed / topSpeed);
            float needleAngle = Mathf.Lerp(stopAngle, topSpeedAngle, speedFraction);
            GUIUtility.RotateAroundPivot(needleAngle, centre);
            GUI.DrawTexture(new Rect(centre.x, centre.y - needleTex.height / 2f, needleTex.width / 1.5f, needleTex.height), needleTex);
            GUI.matrix = savedMatrix;
        }
    }
}
