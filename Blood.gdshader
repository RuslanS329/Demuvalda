shader_type spatial;
uniform float roughness;
uniform vec3 albedo;
uniform sampler2D noise;
uniform sampler2D noise2;
uniform float speed1;
uniform float speed2;
uniform vec2 dir1;
uniform vec2 dir2;
 
void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	
	vec3 normal1 = texture(noise,UV + TIME * speed1 * dir1).rgb;
	vec3 normal2 = texture(noise2,UV + TIME *speed2 * dir2).rgb;
	vec3 normal = mix(normal1,normal2,0.5);
	NORMAL_MAP = normal;
	ROUGHNESS = roughness;
	ALBEDO = albedo;
	// Called for every pixel the material is visible on.
}

//void light() {
//	// Called for every pixel for every light affecting the material.
//	// Uncomment to replace the default light processing function with this one.
//}
