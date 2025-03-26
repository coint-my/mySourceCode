#version 330

out vec4 outputColor;

uniform vec4 ourColor;
in vec2 texCoord;

//const float offset = 1.0 / 128.0;

uniform sampler2D texture0;
uniform sampler2D texture1;

void main()
{
	//vec4 col = texture(texture0, texCoord);
	//if (col.a > 0.5)
	//	outputColor = col;
	//else {
	//	float a = texture(texture0, vec2(texCoord.x + offset, texCoord.y)).a +
	//		texture(texture0, vec2(texCoord.x, texCoord.y - offset)).a +
	//		texture(texture0, vec2(texCoord.x - offset, texCoord.y)).a +
	//		texture(texture0, vec2(texCoord.x, texCoord.y + offset)).a;
	//	if (col.a < 1.0 && a > 0.0)
	//		outputColor = vec4(0.0, 0.0, 0.0, 0.8);
	//	else
	//		outputColor = col;
	//}


    	outputColor = mix(texture(texture0, texCoord), texture(texture1, texCoord), 0.2) * ourColor;
	//outputColor = texture(texture0, texCoord) * texture(texture1, texCoord) * ourColor;
}