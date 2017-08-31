// Stadium
// - parsed & outputted into HLSL or GLSL

include "lighting.inc"      : (SM_TRANSFORM_AND_SHADE)
include "shadow.inc"		: (!low_detail && ps_version >= 2.0 && SM_TRANSFORM_AND_SHADE)
include "fog.inc"			: (SM_TRANSFORM_AND_SHADE)
include "depth.inc"

vertex_streams
{
	v4 position;
	v3 normal;	
	v2 uv;		
}

constants
{
	// General transform
	m44		world_view_projection;
	v4		uv_clip;
}

interpolators
{
	v4		position;
	v3		normal;
	v4		colour_depth;
	v4		uv_misc;
	v2		postproj_coords, enabled=(!low_detail && (ps_version >= 2.0 && SM_TRANSFORM_AND_SHADE));
}

samplers
{
	sampler2D	diffuse_texture;
}

v4 transform_coord( const v3 pos_in )
{
	return mul( v4(pos_in,1.0), world_view_projection );
}

main_vs 
{
	v3 pos				= input.position.xyz;
	v4 world_pos = mul( v4(pos,1.0), world_transform );
	v4 transformed_pos	= transform_coord( pos );
	float y_param = (world_pos.y * uv_clip.x);
	float above_ground = world_pos.y > 1.0 ? 0.0 : 1.0;

	v4 colour_and_weight = static_lighting(pos, input.normal, false);

	float depth = calculate_depth( transformed_pos.z );

	output.normal = normalize( v3(  dot( input.normal, world_inverse_transpose[0].xyz),
				dot( input.normal, world_inverse_transpose[1].xyz),
				dot( input.normal, world_inverse_transpose[2].xyz) ));

	output.uv_misc       = v4( input.uv, depth, colour_and_weight.a);
	output.colour_depth = v4(colour_and_weight.rgb, calculate_fogdepth(transformed_pos.w) );
	output.position		= transformed_pos;

	// additional effects for sm 2.0 and above

	if : (!low_detail && (ps_version >= 2.0 && SM_TRANSFORM_AND_SHADE))
	{
		setup_static_shadow(v4(pos,1.0));
		output.postproj_coords = transformed_pos.xy / transformed_pos.w;
	}
}

main_ps
{
	v4 sampled_pixel = tex2D(diffuse_texture, input.uv_misc.xy); // texture diffuse

	sampled_pixel.rgb = lerp( diffuse.rgb, sampled_pixel.rgb,  sampled_pixel.a ) * input