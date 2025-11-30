shader_type canvas_item;

uniform float disappearX;
uniform sampler2D text : repeat_enable;
uniform float value1;
uniform float value2;
uniform vec4 color1 : source_color;
uniform vec4 color2 : source_color;
uniform sampler2D noise : repeat_enable;
void vertex() {
	
}

void fragment() {
	
	vec2 uv = UV;
	vec4 col;
	float alpha = texture(text,uv).r;
	float no = texture(noise,uv +TIME * 0.05).r;
	
	
	vec3 masd = color1.rgb + no;
	col.rgb = masd;
	col.a = alpha;
	if(SCREEN_UV.x>disappearX){
		col.a = 0.0;
	}
	COLOR = col;
	// Called for every pixel the material is visible on.
	
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
