shader_type spatial;
uniform sampler2D noise;
uniform sampler2D texturef;
void vertex() {
	vec3 pos = VERTEX;
	vec2 uv = UV * 0.3 + sin(TIME * 0.3f);
	vec3 n = texture(noise,uv).rgb * 0.1f;
	
	pos.x+= n.r;
	pos.z+= n.g;
	VERTEX = pos;
	
	
	// Called for every vertex the material is visible on.
}

void fragment() {
	ALBEDO = texture(texturef,UV).rgb;
	// Called for every pixel the material is visible on.
}

//void light() {
//	// Called for every pixel for every light affecting the material.
//	// Uncomment to replace the default light processing function with this one.
//}
