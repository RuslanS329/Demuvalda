shader_type canvas_item;
uniform sampler2D screen_texture : hint_screen_texture;
const int radius = 8;
void vertex() {
	// Called for every vertex the material is visible on.
}
float intensity(vec3 color){
	return color.r * 0.2126 + color.b * 0.7152 + color.g * 0.0722;
}
vec4 diveation(vec2 corner1,vec2 corner2){

	vec3[radius * radius] region;
	float[radius*radius] dispersion;
	int i = 0;
	for(float x = corner1.x; x < corner2.x;x++){
		for(float y = corner1.y; y < corner2.y;y++){
			region[i++] = texture(screen_texture,vec2(x,y)).rgb;

		}
	}
	float avg = 0.0;
	//AVG INTENSITY
	for(int a = 0; a < region.length(); a++){
		avg = intensity(region[a]);
	}
	avg/=float(radius)*float(radius);
	//DISPERSION
	for(int a = 0; a < region.length(); a++){
		dispersion[a] = (avg - intensity(region[a]));
	}
	//AVG DISPERSION
	float avg_dispersion = 0.0;
	for(int a = 0; a < region.length(); a++){
		avg_dispersion +=dispersion[a];
	}
	
	vec3 color;
	for(int a = 0; a < region.length(); a++){
		color+=region[a];
	}
	color/=float(region.length());
	return vec4(color,avg_dispersion);
}

void fragment() {
	vec4 color = texture(screen_texture,UV);
	//quad 1
	float rad = float(radius);
	vec4 c1 = diveation(vec2(0,0),vec2(rad,rad));
	//vec4 c2 = diveation(vec2(-rad,0),vec2(0,rad));
	//vec4 c3 = diveation(vec2(0,0),vec2(rad,rad));
	//vec4 c4 = diveation(vec2(0,-rad),vec2(rad,0));
	
	color.rgb = c1.rgb;
	
	//if(c1.a > c2.a){
		//color.rgb = c2.rgb;
	//}
	//if(c2.a > c3.a){
		//color.rgb = c3.rgb;
	//}
	//if(c3.a > c4.a){
		//color.rgb = c4.rgb;
	//}
	
	// Called for every pixel the material is visible on.
	COLOR = color;


//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
}