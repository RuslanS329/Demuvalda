shader_type canvas_item;
uniform float BLUR_RADIUS = 4.0;
uniform sampler2D screen_texture : hint_screen_texture;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {

	vec4 col = textureLod(screen_texture,UV,BLUR_RADIUS);
	COLOR = col;

}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
