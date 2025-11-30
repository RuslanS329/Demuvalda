shader_type canvas_item;
uniform sampler2D screen_texture : hint_screen_texture;
float intensity(vec3 color){
	return color.r * 0.2126 + color.b * 0.7152 + color.g * 0.0722;
}
void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec3 tex = textureLod(screen_texture,UV,0.1).rgb;
	COLOR = vec4(tex,1.0);
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
