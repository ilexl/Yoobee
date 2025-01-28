shader_type canvas_item;

uniform float blur_power: hint_range(0.0, 0.2, 0.001) = 0.05;
uniform sampler2D noise_texture;

vec2 noise(vec2 uv) {
	return texture(noise_texture, uv).xy - 0.5;
}

void fragment() {
	vec2 uv = UV;
	COLOR = texture(TEXTURE, uv + noise(uv) * blur_power);
}
