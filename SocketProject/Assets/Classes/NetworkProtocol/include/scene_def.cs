using System;

namespace twp
{
	namespace app
	{
		namespace scene
		{
			 enum ELimit
            {
                LIMIT_SCENE_NAME_LENGTH = 30,       // 场景名称长度
                INVALID_SCENE_SET_ID = 0xffff,      // 非法场景组ID
                DYNLOAD_SCENE_ZONE_ID = 999999,     // 动态加载的场景区域id(所有ss都需要负载的场景)
                MAX_PER_INSTANCE_SCENE_COUNT = 32,  // 每个副本中最大场景数
                MAX_PER_GET_INSTANCE_LIST = 64,     // 单次获取的副本种类最大数量
                MAX_SELECTINSTANCE_LIST_COUT = 128, // 最多选择副本列表项
                INVALID_SCENE_ID = 0xFFFF/*ffff*/,      // 非法场景类型ID
                MAPBLOCK_SIZE = 1024000,			//	单位：毫米，面积为：SCENE_MAPBLOCK_SIZE * SCENE_MAPBLOCK_SIZE


                INVALID_SCENE_OBJ_ID = 0,               // 无效场景实例ID
                INSTANCE_SCENE_TYPE_BEGIN_ID = 9000,	// 副本场景开始类型ID

                INVALID_INSTANCE_SELECT_ID  = 0xffff,       // 非法副本选择id
                // 最大进入副本条件数量
                MAX_ENTER_INSTANCE_AMOUNT = 4, 
				MAX_REWARD_LOTTERY_BOX_AMOUNT = 3, 

				ROT_ITEM_WAR_SCENE_EXPEND_ITEM_ID = 11046,

				// 非法动态负载组id
				INVALID_MAINWORLD_SCENE_DYNAMIC_LOAD_BALANCING_SET_ID = 0xffff,
            };
			
			public enum InstanceRuleType
			{
                IRT_INVALID = 0,
                // 普通的关卡（PVE）
                IRT_GENERAL = 1,
				// 对战的关卡（PVP）
				IRT_PVP = 2,
			};
			
			public class Location
			{
				public Position pos;
				public UInt32 scene_type_idx;
				public UInt64 scene_obj_idx;
				
				public Location()
				{
					pos = new twp.app.Position();
				}
				
				public byte[] ToBin()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray();
					bin.Put(pos.x);
					bin.Put(pos.y);
					bin.Put(scene_type_idx);
					bin.Put(scene_obj_idx);
					return bin.GetData();
				}
				
				public void FromBin(NetSocket.ByteArray bin)
				{
					bin.Get_(out pos.x);
					bin.Get_(out pos.y);
					bin.Get_(out scene_type_idx);
					bin.Get_(out scene_obj_idx);
				}
			}
			
			public class CreateInstanceRule
			{
				// 对应InstanceRuleType
				public int instance_rule_type;
				// 最多允许进入人数
				public byte max_allow_player_count;
				
				// 防御方
				public byte defense_area_idx;
				public UInt64 defense_city_idx;
				
				public byte[] ToBin()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray();
					bin.Put(instance_rule_type);
					bin.Put(max_allow_player_count);
					bin.Put(defense_area_idx);
					bin.Put(defense_city_idx);
					return bin.GetData();
				}
				
				public void FromBin(NetSocket.ByteArray bin)
				{
					bin.Get_(out instance_rule_type);
					bin.Get_(out max_allow_player_count);
				}
			};
			
			//
			// 移动标示
			//
			public enum MoveFlag : byte
			{
				MOVE_FLAG_TYPE_OVER = 0,// 移动结束(开始自动锁定攻击目标)
				MOVE_FLAG_TYPE_ING = 1, // 正在移动(不需要自动锁定攻击目标)
				MOVE_FLAG_TYPE_MAX,
			};
		}
	}
}

