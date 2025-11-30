shader_type canvas_item;
uniform sampler2D screen : hint_screen_texture;
uniform float levels = 4.0;

void vertex() {
	
}

void fragment() {
	vec4 col = texture(screen,UV);
	float value = col.r + col.g + col.b; 
	float new_value = round(value * levels) / levels;
	
	col = col - value + new_value;
	
	COLOR = col;
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
