Heavy Figher Cockpit

Model:
The FBX contains 4 meshes: Body, Joystick, ThrottleControl, Glass

****Textures:
This asset contains 2 main materials: Body and Glass

- The body texures come in 3 variants (Clean, Weathered, Rusty).
Each variant has it's own Albedo, Metallic, Roughness and Normal Textures.
AmbientOcclusion and Emission are shared between all variants.
For each variant, a PSD file is provided for the Albedo texture (Customize metal and leather color)
The Emissive texture is also a PSD (Customize screen colors)

- The glass textures come in 4 variants (Clean, Dirty, Scratched, Dirty&Scratched)
The AmbientOcclusion texture is shared between all variants.

****Texture naming
All textures have their designated channel in the file name (ex: _Metallic)
Some textures have 2 channel names in their name (ex: _MetallicRoughness).
These textures use the first channel (ex: Metallic) in the RGB, and the second (ex: Roughness) in the Alpha.
Though each PBR channel is provided separately aswell

****Additional channels:
Glossy or Smoothness or Shininess or sometimes Specular textures (Depending on software) are obtained by inverting the Roughness texture.
Opacity is obtained by inverting the Transparent texture.