shader_type spatial;
uniform sampler2D screen_texture : hint_screen_texture;
uniform sampler2D depth_texture : hint_depth_texture;
render_mode unshaded,fog_disabled;
void vertex() {
  POSITION = vec4(VERTEX.xy, 1.0, 1.0);
}

void fragment() {
  float depth = texture(depth_texture, SCREEN_UV).x;
  vec3 ndc = vec3(SCREEN_UV * 2.0 - 1.0, depth);
  vec4 view = INV_PROJECTION_MATRIX * vec4(ndc, 1.0);
  view.xyz /= view.w;
  float linear_depth = -view.z;

  

  // Visualize linear depth
  ALBEDO.rgb = vec3(fract(linear_depth));


 
}