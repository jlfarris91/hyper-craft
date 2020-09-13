using System;
using System.Linq;
using CommonLib;
using UnityEngine;

public class Body : MonoBehaviour
{
    public BodyDefinition definition;

    public void ApplyDefinition(BodyDefinition definition)
    {
        this.definition = definition;
        BroadcastMessage(Messages.BodyDefinitionChanged);
    }

    public GameObject[] GetHardPoints(string hardPointName)
    {
        return transform.FindChildren(hardPointName, true)
            .Where(IsHardPoint)
            .Select(child => child.gameObject).ToArray();
    }

    private static bool IsHardPoint(Transform hardpoint)
    {
        return !hardpoint.HasComponent<MeshRenderer>();
    }

    private void Start()
    {
        ApplyDefinition(definition);
    }

    public static class Messages
    {
        public static readonly string BodyDefinitionChanged = "BodyDefinitionChanged";
    }
}