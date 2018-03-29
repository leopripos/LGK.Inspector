# LGK.Inspector
=====================================

This is simple inspector for Unity Game Engine for Svelto.ECS (https://github.com/sebas77/Svelto.ECS)

## Feature
* Svelto.ECS style (without MonoBehaviour)
* Using Reflection
* Support field
* Support property (setter/getter only)
* Support custom component drawer (like unity custom inspector)
* Support custom type drawer (like unity property drawer)
* \** Not Support colletion (array/struct)
* \** Can't automatically draw class or struct (you need create your own type drawer)

\** need suggestion how to implement this :D

## Usage
```csharp
public interface IInspectorService
{
    void Register(int id, object[] components, byte modeMask = EInspectorMode.All);

    void Register(string group, int id, object[] components, byte modeMask = EInspectorMode.All);

    void Register(string group, IEntityInfo entityInfo);

    void Unregister(int id);

    void Unregister(string group, int id);
}

public static class EInspectorMode
{
    public const byte All = byte.MaxValue;
    public const byte Field = 1;
    public const byte Property = 1 << 1;
}
```

## Examples
* Simple Example **: https://github.com/NED-Studio/LGK.Inspector.Example
