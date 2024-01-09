# tileGameScripts
A set of scripts for fast prototyping of management/RTS games.
Requires Cinemachine and LeanTween.

Proposed GO/Monobehavior structure:
+ Ubik (Global singleton)
+ Main (Scene-specific singleton)
    + Audio
    + TileDaimon
    + TimeDaimon
    + BlockDaimon
+ Stage (Set on Canvas object)
+ Rig
    + Camera
    + Cinemachine virtual camera
    + Camera pointer

Invokable is a wrapper class for scriptable objects, made so they can be reached more easily via Auxiliary.GetInvokable methods
Parsing is a static class which parses text, requirements, and arguments - be it on their own or bundled as Instructions. 
