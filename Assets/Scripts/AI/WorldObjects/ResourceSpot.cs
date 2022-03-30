using System;
public class ResourceSpot : GameResourceController
{
    public int Workers { get; set; }    // Current workers assigned to this resource

    public override void Start()
    {
        base.Start();

        AIManager.Instance.RegisterResourceSpot(this);
        Workers = 0;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        AIManager.Instance.ConsumeResourceSpot(this);
    }
}

