/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 80019
 Source Host           : localhost:3306
 Source Schema         : school_admin

 Target Server Type    : MySQL
 Target Server Version : 80019
 File Encoding         : 65001

 Date: 15/05/2020 17:34:22
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for charge_account
-- ----------------------------
DROP TABLE IF EXISTS `charge_account`;
CREATE TABLE `charge_account`  (
  `id` bigint(0) NOT NULL COMMENT 'id',
  `base_is_delete` int(0) NULL DEFAULT NULL COMMENT '	删除标记(0正常 1删除)',
  `base_create_time` datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
  `base_modify_time` datetime(0) NULL DEFAULT NULL COMMENT '	修改时间',
  `base_creator_id` bigint(0) NULL DEFAULT NULL COMMENT '创建人',
  `base_modifier_id` bigint(0) NULL DEFAULT NULL COMMENT '修改人',
  `base_version` int(0) NULL DEFAULT NULL COMMENT '数据版本(每次更新+1)',
  `sys_department_id` bigint(0) NULL DEFAULT NULL COMMENT '部门id',
  `sys_department_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '部门名称',
  `date` datetime(0) NULL DEFAULT NULL COMMENT '对账日期',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '对账人',
  `result` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '对账结果',
  `status` int(0) NULL DEFAULT NULL COMMENT '状态',
  `charge_start_date` datetime(0) NULL DEFAULT NULL COMMENT '收费开始时间',
  `charge_end_date` datetime(0) NULL DEFAULT NULL COMMENT '收费结束时间',
  `charge_money` decimal(10, 2) NULL DEFAULT NULL COMMENT '收费金额',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '收费对账记录' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for charge_details
-- ----------------------------
DROP TABLE IF EXISTS `charge_details`;
CREATE TABLE `charge_details`  (
  `id` bigint(0) NOT NULL COMMENT 'id',
  `base_is_delete` int(0) NULL DEFAULT NULL COMMENT '	删除标记(0正常 1删除)',
  `base_create_time` datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
  `base_modify_time` datetime(0) NULL DEFAULT NULL COMMENT '	修改时间',
  `base_creator_id` bigint(0) NULL DEFAULT NULL COMMENT '创建人',
  `base_modifier_id` bigint(0) NULL DEFAULT NULL COMMENT '修改人',
  `base_version` int(0) NULL DEFAULT NULL COMMENT '数据版本(每次更新+1)',
  `sys_department_id` bigint(0) NULL DEFAULT NULL COMMENT '部门id',
  `sys_department_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '部门名称',
  `charge_sheet_id` bigint(0) NULL DEFAULT NULL COMMENT '收费单id',
  `charge_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '收费单号',
  `student_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '学生编号',
  `details_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '收费明细名称',
  `money` decimal(10, 2) NULL DEFAULT NULL COMMENT '收费金额',
  `status` int(0) NULL DEFAULT 1 COMMENT '状态：1正常，2已缴费',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '收费单明细' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for charge_record
-- ----------------------------
DROP TABLE IF EXISTS `charge_record`;
CREATE TABLE `charge_record`  (
  `id` bigint(0) NOT NULL COMMENT 'id',
  `base_is_delete` int(0) NULL DEFAULT NULL COMMENT '	删除标记(0正常 1删除)',
  `base_create_time` datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
  `base_modify_time` datetime(0) NULL DEFAULT NULL COMMENT '	修改时间',
  `base_creator_id` bigint(0) NULL DEFAULT NULL COMMENT '创建人',
  `base_modifier_id` bigint(0) NULL DEFAULT NULL COMMENT '修改人',
  `base_version` int(0) NULL DEFAULT NULL COMMENT '数据版本(每次更新+1)',
  `sys_department_id` bigint(0) NULL DEFAULT NULL COMMENT '部门id',
  `sys_department_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '部门名称',
  `charge_sheet_id` bigint(0) NULL DEFAULT NULL COMMENT '收费单id',
  `charge_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '收费单号',
  `trade_no` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '交易单号',
  `student_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '学生编号',
  `money` decimal(10, 2) NULL DEFAULT NULL COMMENT '收费金额',
  `charge_date` datetime(0) NULL DEFAULT NULL COMMENT '收费时间',
  `user_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '收费人员名称',
  `type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '收费类型',
  `invoice_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '单据号',
  `remarks` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `status` int(0) NULL DEFAULT NULL COMMENT '记录状态：1正常，0作废',
  `type_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '收费类型名称',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '收费记录' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for charge_sheet
-- ----------------------------
DROP TABLE IF EXISTS `charge_sheet`;
CREATE TABLE `charge_sheet`  (
  `id` bigint(0) NOT NULL COMMENT 'id',
  `base_is_delete` int(0) NULL DEFAULT NULL COMMENT '	删除标记(0正常 1删除)',
  `base_create_time` datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
  `base_modify_time` datetime(0) NULL DEFAULT NULL COMMENT '	修改时间',
  `base_creator_id` bigint(0) NULL DEFAULT NULL COMMENT '创建人',
  `base_modifier_id` bigint(0) NULL DEFAULT NULL COMMENT '修改人',
  `base_version` int(0) NULL DEFAULT NULL COMMENT '数据版本(每次更新+1)',
  `sys_department_id` bigint(0) NULL DEFAULT NULL COMMENT '部门id',
  `sys_department_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '部门名称',
  `charge_no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '收费单号',
  `charge_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '收费名称',
  `status` int(0) NULL DEFAULT 1 COMMENT '状态：0禁用，1启用',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '收费单' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for charge_type_config
-- ----------------------------
DROP TABLE IF EXISTS `charge_type_config`;
CREATE TABLE `charge_type_config`  (
  `id` bigint(0) NOT NULL COMMENT 'id',
  `base_is_delete` int(0) NULL DEFAULT NULL COMMENT '	删除标记(0正常 1删除)',
  `base_create_time` datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
  `base_modify_time` datetime(0) NULL DEFAULT NULL COMMENT '	修改时间',
  `base_creator_id` bigint(0) NULL DEFAULT NULL COMMENT '创建人',
  `base_modifier_id` bigint(0) NULL DEFAULT NULL COMMENT '修改人',
  `base_version` int(0) NULL DEFAULT NULL COMMENT '数据版本(每次更新+1)',
  `sys_department_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '部门名称',
  `type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '类型',
  `type_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '类型名称',
  `no` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '最新编号',
  `status` int(0) NULL DEFAULT 1 COMMENT '状态：0禁用，1启用',
  `sys_department_id` bigint(0) NULL DEFAULT NULL COMMENT '部门id',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '收费类型管理' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for student_info
-- ----------------------------
DROP TABLE IF EXISTS `student_info`;
CREATE TABLE `student_info`  (
  `id` bigint(0) NOT NULL COMMENT 'id',
  `base_is_delete` int(0) NULL DEFAULT NULL COMMENT '	删除标记(0正常 1删除)',
  `base_create_time` datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
  `base_modify_time` datetime(0) NULL DEFAULT NULL COMMENT '	修改时间',
  `base_creator_id` bigint(0) NULL DEFAULT NULL COMMENT '创建人',
  `base_modifier_id` bigint(0) NULL DEFAULT NULL COMMENT '修改人',
  `base_version` int(0) NULL DEFAULT NULL COMMENT '数据版本(每次更新+1)',
  `sys_department_id` bigint(0) NULL DEFAULT NULL COMMENT '部门id',
  `sys_department_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '部门名称',
  `code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '学生编码',
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '学生姓名',
  `class` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '班级',
  `phone` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '手机号码',
  `openid` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '绑定微信开放id',
  `status` int(0) NULL DEFAULT 0 COMMENT '状态：0禁用，1启用',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '学生信息' ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
