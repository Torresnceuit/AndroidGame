// Stadium ads shader
include "lighting.inc"  
include "shadow.inc"		: (!low_detail && ps_version >= 2.0 && SM_TRANSFORM_AND_SHADE) 
include "fog.inc"
include "depth.inc"

vertex_streams
{
	v3 position;
	v2 uv;
}

constants
{
	m44 world_view_projection;
}

interpolators
{
	v4 position;
	v3 uv_depth;
	v4 colour_depth;
}

samplers
{
	sampler2D	diffuse_texture;
}

main_vs
{
	v3 world_pos = input.position;
	v4 transformed_pos = mul( v4(input.position,1.0), world_view_projection );

	output.position = transformed_pos;
	output.uv_depth.xy = input.uv;

	float fade = clamp( floodlighting(world_pos) * 2.0, 0.25, 1.0 );
	output.colour_depth = v4(fade, fade, fade, calculate_fogdepth(transformed_pos.w));
	
	output.uv_depth.z = calculate_depth( transformed_pos.z );

	if : (!low_detail && vs_version >= 2.0 && SM_TRANSFORM_AND_SHADE) 
	{
		// additional effects for sm 2.0 and above
		setup_static_shadow(v4(world_pos,1.0));
	}
}

main_ps
{
	v4 sampled_pixel  = tex2D(diffuse_texture, input.uv_depth.xy); // texture diffuse


	if : (!low_deta