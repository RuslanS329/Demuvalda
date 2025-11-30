shader_type spatial;
render_mode unshaded;
uniform sampler2D DEPTH_TEXTURE : hint_depth_texture,filter_linear_mipmap;
uniform sampler2D SCREEN_TEXTURE : hint_screen_texture,filter_nearest;
uniform sampler2D NORMAL_TEXTURE : hint_normal_roughness_texture,filter_nearest;
uniform vec2 size;

vec3 GetNormal(vec2 uv, sampler2D normalTexture){
	vec3 normal = texture(normalTexture,uv).rgb;
	normal = normal * 2.0 - 1.0;
	return normal;
}
float GetLinearDepth(vec2 sUV,sampler2D depthTexture, mat4 invProjectionMat){
	float depth = texture(depthTexture,sUV).x;
	vec3 ndc = vec3(sUV * 2.0 - 1.0,depth);
	vec4 view = invProjectionMat * vec4(ndc,1.0);
	view.xyz /= view.w;
	return -view.z;
}

void vertex() {
	//POSITION = vec4(VERTEX.xy,1.0,1.0);
	// Called for every vertex the material is visible on.
}

void fragment() {
	
	
	vec2 texelSize = size / VIEWPORT_SIZE.xy;
	vec2 sUV = SCREEN_UV;
	
	vec2 UVOffsets4[4];
	UVOffsets4[0] = vec2(-1.0,0.0);
	UVOffsets4[1] = vec2(1.0,0.0);
	UVOffsets4[2] = vec2(0.0,-1.0);
	UVOffsets4[3] = vec2(0.0,1.0);
	float depth = GetLinearDepth(sUV,DEPTH_TEXTURE,INV_PROJECTION_MATRIX);
	float depthDifference = 0.0;
	for(int i = 0; i < UVOffsets4.length(); i++){
		float dOff = GetLinearDepth(sUV + UVOffsets4[i] * texelSize,DEPTH_TEXTURE,INV_PROJECTION_MATRIX);
		depthDifference +=clamp(dOff - depth,0.0,1.0);
	}
	vec3 normal = GetNormal(sUV,NORMAL_TEXTURE);
	float normalDifference = 0.0;
	for(int i = 0; i < UVOffsets4.length(); i++){
		vec3 nOff = GetNormal(sUV + UVOffsets4[i] * texelSize,NORMAL_TEXTURE);
		normalDifference += 1.0 - dot(normal,nOff);
	}
	normalDifference = smoothstep(0.2,0.2,normalDifference);
	depthDifference = smoothstep(0.25,0.3,depthDifference);
	vec3 outline = vec3(depthDifference);
	


	float x = float(int(FRAGCOORD.x) % int(size.x));
	float y = float(int(FRAGCOORD.y) % int(size.y));
	x = FRAGCOORD.x + floor(float(size.x) / 2.0) - x;
	y = FRAGCOORD.y + floor(float(size.y) / 2.0) - y;
	sUV = vec2(x,y)/VIEWPORT_SIZE;
	
	vec3 color = texture(SCREEN_TEXTURE,sUV).rgb;
	
	
	//color = mix(color, vec3(0.0),outline);
	ALBEDO = color;
}

//void light() {
//	// Called for every pixel for every light affecting the material.
//	// Uncomment to replace the default light processing function with this one.
//}
