# tileGameScripts
A set of scripts for fast prototyping of management/RTS games.
Requires Cinemachine and LeanTween.

Proposed GO structure:
+ Ubik (Global singleton)
+ Main (Scene-specific singleton)
    + Audio
    + TileDaimon
    + TimeDaimon
+ Stage (Set on Canvas object)
+ Rig
    + Camera
    + Cinemachine virtual camera
    + Camera pointer

